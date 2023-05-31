using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Model;
using System.Net.Http;
using Newtonsoft.Json;
using System.Text;
using System.Net.Http.Headers;

namespace Front_GSB.Controllers
{
    public class MedecinsController : Controller
    {

        // GET: Medecins
        public async Task<ActionResult> Index()
        {
            //L'API a consommer depuis le back
            string url = "https://localhost:44336/api/Medecins";

            using (HttpClient client = new HttpClient())
            {

                HttpResponseMessage response = await client.GetAsync(url);

                // si erreur, on propage une exeception
                if (!response.IsSuccessStatusCode)
                    throw new Exception();

                //la liste des médecins
                var med = await response.Content.ReadAsAsync<IEnumerable<Medecin>>();

                return View(med);
            }
            //var medecins = db.Medecins.Include(m => m.Departement);
            //return View(await medecins.ToListAsync());
        }

        // GET: Medecins/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            //L'API a consommer depuis le back
            string url = "https://localhost:44336/api/Medecins/" + id;

            using (HttpClient client = new HttpClient())
            {

                HttpResponseMessage response = await client.GetAsync(url);

                // si erreur, on propage une exeception
                if (!response.IsSuccessStatusCode)
                    throw new Exception("Un problème c'est produit, veuillez contacter un administrateur");

                //la liste des médecins
                var med = await response.Content.ReadAsAsync<Medecin>();

                return View(med);
            }

            //Medecin medecin = await db.Medecins.FindAsync(id);
            //if (medecin == null)
            //{
            //    return HttpNotFound();
            //}
            //return View(medecin);
        }

        // GET: Medecins/Create
        [Authorize]
        public async Task<ActionResult> Create()
        {
            string urlDepatements = "https://localhost:44336/api/Departements";
            using (HttpClient client = new HttpClient())
            {
                //Pour s'authentifier sur le back-end avec le token
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json")); //forcer le content-type
                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + ReadToken());

                HttpResponseMessage response = await client.GetAsync(urlDepatements);

                //si erreur, on propage une execption
                if (!response.IsSuccessStatusCode)
                    throw new Exception();

                //la liste des département
                var dep = response.Content.ReadAsAsync<IEnumerable<Departement>>().Result.ToList();

                //pour avoir la liste des departements lors de la création d'un médecin
                ViewBag.dep_numero = new SelectList(dep, "dep_numero", "dep_nom");
                return View();
            }

        }

        // POST: Medecins/Create
        // Pour vous protéger des attaques par survalidation, activez les propriétés spécifiques auxquelles vous souhaitez vous lier. Pour 
        // plus de détails, consultez https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "med_id,med_nom,med_prenom,med_addresse,med_telephone,med_specialite,dep_numero")] Medecin medecin)
        {
            if (ModelState.IsValid)
            {
                //on récupère les infos saisies dans l'interface et les sérialiser en json
                string json = JsonConvert.SerializeObject(medecin);

                using (HttpClient client = new HttpClient()) // utilisation client HTTP
                {
                    //Pour s'authentifier sur le back-end avec le token
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json")); //forcer le content-type
                    client.DefaultRequestHeaders.Add("Authorization", "Bearer " + ReadToken());

                    //client.DefaultRequestHeaders.Add("token","token");
                    using (var request = new HttpRequestMessage(HttpMethod.Post, "https://localhost:44336/api/Medecins")) //l'objet Request
                    {
                        request.Content = new StringContent(json, Encoding.UTF8, "application/json");
                        //envoi des infos
                        var send = await client.SendAsync(request, HttpCompletionOption.ResponseHeadersRead).ConfigureAwait(false);

                        if (!send.IsSuccessStatusCode) // si une erreur, on propage une exeption
                            throw new Exception("Un problème c'est produit, veuillez contacter un administrateur");

                        send.EnsureSuccessStatusCode();
                        //ViewBag.dep_numero = new SelectList(db.Departements, "dep_numero", "dep_nom", medecin.dep_numero);
                        return RedirectToAction("Index");
                    }
                }
            }


            //if (ModelState.IsValid)
            //{
            //    db.Medecins.Add(medecin);
            //    await db.SaveChangesAsync();
            //    return RedirectToAction("Index");
            //}

            //ViewBag.dep_numero = new SelectList(db.Departements, "dep_numero", "dep_region", medecin.dep_numero);
            return View(medecin);
        }

        // GET: Medecins/Edit/5
        [Authorize]
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            //L'API a consommer depuis le back
            string url = "https://localhost:44336/api/Medecins/" + id;
            string urlDepartements = "https://localhost:44336/api/Departements";


            using (HttpClient client = new HttpClient())
            {
                //Pour s'authentifier sur le back-end avec le token
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json")); //forcer le content type json
                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + ReadToken());

                HttpResponseMessage response = await client.GetAsync(url);
                HttpResponseMessage responsedep = await client.GetAsync(urlDepartements);

                // si erreur, on propage une exeception
                if (!response.IsSuccessStatusCode || !responsedep.IsSuccessStatusCode)
                    throw new Exception("Un problème c'est produit, veuillez contacter un administrateur");

                //la liste des médecins
                var medecin = await response.Content.ReadAsAsync<Medecin>();
                //la liste des département
                var dep = responsedep.Content.ReadAsAsync<IEnumerable<Departement>>().Result.ToList();


                ViewBag.dep_numero = new SelectList(dep, "dep_numero", "dep_nom", medecin.dep_numero);
                return View(medecin);
            }
            
        }

        // POST: Medecins/Edit/5
        // Pour vous protéger des attaques par survalidation, activez les propriétés spécifiques auxquelles vous souhaitez vous lier. Pour 
        // plus de détails, consultez https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "med_id,med_nom,med_prenom,med_addresse,med_telephone,med_specialite,dep_numero")] Medecin medecin)
        {


            if (ModelState.IsValid)
            {
                //onrécupère les infos dans l'interface et les sérialiser en json
                string json = JsonConvert.SerializeObject(medecin);

                using (HttpClient client = new HttpClient()) // utilisation client HTTP
                {
                    //Pour s'authentifier sur le back-end avec le token
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json")); //forcer le content-type
                    client.DefaultRequestHeaders.Add("Authorization", "Bearer " + ReadToken());

                    //client.DefaultRequestHeaders.Add("token", "token");
                    HttpContent cont = new StringContent(json, Encoding.UTF8, "application/json");
                    // envoie des infos
                    var send = await client.PutAsync("https://localhost:44336/api/Medecins/" + medecin.med_id, cont).ConfigureAwait(false);

                    if (!send.IsSuccessStatusCode) //si erreur, on propage une exeption
                        throw new Exception();

                    send.EnsureSuccessStatusCode();
                    ////ViewBag.dep_numero = new SelectList(db.Departements, "dep_numero", "dep_nom", medecin.dep_numero);
                    return RedirectToAction("Index");
                }
            }
            return View(medecin);
                //    db.Entry(medecin).State = EntityState.Modified;
                //    await db.SaveChangesAsync();
                //    return RedirectToAction("Index");
                //}
                //ViewBag.dep_numero = new SelectList(db.Departements, "dep_numero", "dep_nom", medecin.dep_numero);
                //return View(medecin);
            }

        // GET: Medecins/Delete/5
        [Authorize]
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            //L'API a consommer depuis le back
            string url = "https://localhost:44336/api/Medecins/" + id;

            using (HttpClient client = new HttpClient())
            {
                //Pour s'authentifier sur le back-end avec le token
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json")); //forcer le content-type
                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + ReadToken());

                HttpResponseMessage response = await client.GetAsync(url);

                // si erreur, on propage une exeception
                if (!response.IsSuccessStatusCode)
                    throw new Exception("Un problème c'est produit, veuillez contacter un administrateur");

                //le médecin
                var medecin = await response.Content.ReadAsAsync<Medecin>();

                return View(medecin);
            }
        }

        // POST: Medecins/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            string url = "https://localhost:44336/api/Medecins/" + id;

            // controle indispensable
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            
            using(HttpClient client = new HttpClient())
            {
                //Pour s'authentifier sur le back-end avec le token
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json")); //forcer le content-type
                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + ReadToken());

                //client.DefaultRequestHeaders.Add("token", "token");
                HttpResponseMessage response = await client.DeleteAsync(url);

                if (!response.IsSuccessStatusCode)
                    throw new Exception();
            }

            return RedirectToAction("Index");
        }

        /// <summary>
        /// lire le token dans le fichier
        /// </summary>
        /// <returns></returns>
        private string ReadToken()
        {
            string token = string.Empty;
            try
            {
                string fileName = @"C:\temp\token.txt";
                token = System.IO.File.ReadAllText(fileName);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            return token;
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                //db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
