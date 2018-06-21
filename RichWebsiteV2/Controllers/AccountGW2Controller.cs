using RichData.GuildWars2;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using RichWebsiteV2.Models;

namespace RichWebsite.Controllers
{
    public class AccountGW2Controller : Controller
    {
        private string _key { get; set; }
        private string[] characterList = null;
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Account()
        {
            return View();
        }

       
        public ActionResult GW2()
        {
            ViewData["Message"] = "Guild";

            return View();
        }

        
        public ActionResult GW21(string apiKey)
        {
            Session["key"] = apiKey;

            return View("Navigation");
        }

        public ActionResult Navigation()
        {
            return View();
        }
        
        
        public ActionResult Items()
        {
            return View();
        }

        public async Task<ActionResult> Chars()
        {
            if (characterList == null)
            {
                _key = Session["key"].ToString();
                var characters = new Characters();
                characterList = await characters.GetCharacterListAsync(_key);
                foreach (var chara in characterList)
                {
                    Session[chara] = chara;
                }
                
                //foreach (string key in Session.Keys)
                //{
                //    Response.Write(key + " - " + Session[key] + "<br />");
                //}

            }
            return View("Chars", characterList);
        }

        public async Task<ActionResult> spezificCharacter(String submit)
        {
            _key = Session["Key"].ToString();
            string name = submit;
            //for (int i = 1; i < Session.Contents.Count; i++)
            //{
            //    characterList[i - 1] = Session[i].ToString();
            //}
            var characters = new Characters();
            var selectedChar = new Character();
            selectedChar = await characters.GetCharacterAsync(name, _key);
            ViewBag.Name = selectedChar.Name;
            ViewBag.
            return View();
        }

    }
}