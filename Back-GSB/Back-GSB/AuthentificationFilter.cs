using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Http.Filters;
using System.Web.Http.Controllers;
using System.Net.Http;

namespace Back_GSB
{
    public class AuthentificationFilter : ActionFilterAttribute
    {
        //Définition d'une variable qui stocke le token
        private string ApiKeyToCheck = "token";

        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            bool validKey = false;

            IEnumerable<string> resquestHeader;

            var checkApiExist = actionContext.Request.Headers.TryGetValues("token", out resquestHeader);

            //je suis ok, le token récupérer depuis le header est authentique
            if (checkApiExist == true)
            {
                if (resquestHeader.FirstOrDefault().Equals(ApiKeyToCheck))
                    validKey = true;
            }

            //si le cas contraire, j'affiche un message accès non autorisé
            if (!validKey)
                actionContext.Response = new HttpResponseMessage(HttpStatusCode.Forbidden); 
        }
    }
}