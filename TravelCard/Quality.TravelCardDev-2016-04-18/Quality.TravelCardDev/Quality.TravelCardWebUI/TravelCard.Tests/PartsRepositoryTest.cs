using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ideal.DomainModel;
using Ideal.DomainModel.Entities;
using Ideal.DomainModel.Repositories;
using Ideal.DomainModel.Abstract;
using IBM.Data.DB2.iSeries;


namespace TravelCard.Tests
{
    /// <summary>
    /// Summary description for UnitTest1
    /// </summary>
    [TestClass]
    public class PartsRepositoryTest
    {
       IPartsRepository<Part> partsRepository;
       Part testPart;
       public IEnumerable<Ideal.DomainModel.Entities.Part> Part { get; set; }
       

        public void RepositoriesTest()
        {
         
        }

        private TestContext testContextInstance;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        #region Additional test attributes

        [TestInitialize()]
        public void CreateRepositories()
        {
          partsRepository = new PartRepository();
     
        }
        
   
        //
        #endregion

        [TestMethod]
        [DeploymentItem("hibernate.cfg.xml")]
        public void CanRetrievePartDetails()
        {
            //this is a test of the parts repository
           testPart = new Part();
           string plantid = "06";
           Part = partsRepository.GetByID("1073000", plantid);
           string partdesc = Part.Select(x => x.ALTDESC).FirstOrDefault();
           
          if (partdesc.Length>0)
          { 
            Assert.IsNotNull(partdesc);
          }
        }
    }
}
