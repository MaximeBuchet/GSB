using Microsoft.VisualStudio.TestTools.UnitTesting;
using ORM_GSB;
using System;
using Model;

namespace Tests_GSB
{
    [TestClass]
    public class TestsORM
    {
        [TestMethod]
        public void TestDepartement()
        {
            GSB_Data_Model orm = new GSB_Data_Model();

            Departement departement = orm.Departements.Find(69);
            Assert.AreEqual("Rhône", departement.dep_nom);
            
        }

        [TestMethod]
        public void TestMedecin()
        {
            GSB_Data_Model orm = new GSB_Data_Model();

            Medecin medecin = orm.Medecins.Find(1);
            Assert.AreEqual("DUPONT", medecin.med_nom);
        }

        [TestMethod]
        public void TestUser()
        {
            GSB_Data_Model orm = new GSB_Data_Model();

            user user = orm.Users.Find(1);
            Assert.AreEqual("test", user.user_pseudo);
        }
    }
}
