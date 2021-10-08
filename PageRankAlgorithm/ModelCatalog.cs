using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PageRankAlgorithm
{
    public class PageNode
    {
        public int Id { get; set; }
        public float PageRank { get; set; } = 1;
        public string Name { get; set; }
        public List<PageNode> Input { get; set; } = new List<PageNode>();
        public List<PageNode> Output { get; set; } = new List<PageNode>();
    }

    public class User
    {
        public int user_id { get; set; }
        public string user_name { get; set; }
        public string user_real_name { get; set; }
        public string user_password { get; set; }
        public string user_newpassword { get; set; }
        public string user_newpass_time { get; set; }
        public string user_email { get; set; }
        public string user_touched { get; set; }
        public string user_token { get; set; }
        public string user_email_authenticated { get; set;}
        public string user_email_token { get; set; }
        public string user_email_token_expire { get; set; }
        public string user_registarion { get; set; }
        public int user_editcount { get; set; }
        public string user_password_expires { get; set; }
    }

    public class PageLinks
    {
        public int pl_from { get; set; }
        public int pl_namespace { get; set; }
        public string pl_title { get; set; }
        public int pl_from_namespace{ get;set;}
    }

    public class Page
    {
        public int page_id { get; set; }
        public int page_namespace { get; set; }
        public string page_title { get; set; }
        public string page_restrictions { get; set; }
        public int page_is_redirect { get; set; }
        public int page_is_new { get; set; }
        public string page_random { get; set; }
        public string page_touched { get; set; }
        public string page_links_updated {get;set;}
        public int page_latest { get; set; }
        public int page_len { get; set; }
        public string page_content_model { get; set; }
        public string page_lang { get; set; }
    }
}
