using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PageRankAlgorithm
{
    class Calculator
    {
        List<PageNode> PageNodes = null;
        SQLiteDB Database = null;
        string commentchar = "#######################";

        public Calculator()
        {
            Initialize();
        }
        public bool Initialize()
        {
            PageNodes = new List<PageNode>();
            return true;
        }

        public void Calculate()
        {

            Console.Write(commentchar + " " + DateTime.Now.ToString("dd.MM.yyyy HH:mm:ss:fff") + " " + commentchar);
            Console.WriteLine();

            Database = new SQLiteDB();

            List<User> users = Get_EntrysUser();
            List<PageLinks> links = Get_EntrysPageLink();
            List<Page> pages = Get_EntrysPages();

            InitializePageNodes(pages);

            GetPageRank(pages, links);

            Console.WriteLine();
            Console.Write(commentchar + " " + DateTime.Now.ToString("dd.MM.yyyy HH:mm:ss:fff") + " " + commentchar);
        }

        private void GetPageRank(List<Page> pages, List<PageLinks> links)
        {
            foreach (Page page in pages)
            {
                PageNode node = PageNodes.Find(c => c.Id == page.page_id);
                List<PageLinks> pagelinks =  links.FindAll(c => c.pl_from == node.Id);
                foreach(PageLinks link in pagelinks)
                {
                    PageNode outgoing = PageNodes.Find(c => c.Name == link.pl_title);
                    node.Output.Add(outgoing);
                }
                
            }
            foreach(Page page in pages)
            {
                PageNode node = PageNodes.Find(c => c.Id == page.page_id);
                List<PageLinks> pagelinks = links.FindAll(c => c.pl_title == node.Name);
                foreach (PageLinks link in pagelinks)
                {
                    PageNode input = PageNodes.Find(c => c.Id == link.pl_from);
                    node.Input.Add(input);
                   
                }
            }
            for(int i = 0;i < 1000;)
            {
                foreach (PageNode node in PageNodes)
                {
                    Calculate_PageRank(node);
                    
                }
                i++;
            }
            PageNodes = PageNodes.OrderByDescending(c => c.PageRank).ToList();
            PrintScoreboard();
        }

        private void PrintScoreboard()
        {
            TableDrawer drawer = new TableDrawer();
            drawer.PrintLine();
            drawer.PrintRow("Rank", "Id", "Name", "Score");
            drawer.PrintLine();

            int rank = 1;
            foreach (PageNode node in PageNodes)
            {
                drawer.PrintRow(rank.ToString(), node.Id.ToString(), node.Name.ToString(), node.PageRank.ToString());
                rank++;
            }

            drawer.PrintLine();
        }

        private void Calculate_PageRank(PageNode node)
        {
            node.PageRank = (1 - (float)0.85) + (float)0.85 * CalculateTempSum(node);
        }

        private float CalculateTempSum(PageNode node)
        {
            float sum = 0;

            foreach(PageNode input in node.Input)
            {
                sum = sum + (input.PageRank / input.Output.Count);
            }

            return sum;
        }

        private void InitializePageNodes(List<Page> pages)
        {
            foreach(Page page in pages)
            {
                PageNode node = new PageNode();
                node.Id = page.page_id;
                node.Name = page.page_title;
                PageNodes.Add(node);
            }
        }

        private List<User> Get_EntrysUser()
        {
            
            Console.WriteLine();
            Console.WriteLine("start searching entrys User");
            

            return Database.con.Table<User>().ToList();
        }

        private List<PageLinks> Get_EntrysPageLink()
        {

            Console.WriteLine();
            Console.WriteLine("start searching entrys Pagelink");
            

            return Database.con.Table<PageLinks>().ToList();
        }

        private List<Page> Get_EntrysPages()
        {

            Console.WriteLine();
            Console.WriteLine("start searching entrys Pages");
            Console.WriteLine();


            return Database.con.Table<Page>().ToList();
        }
    }
}

