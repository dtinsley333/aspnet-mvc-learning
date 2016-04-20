using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ZoolandiaZooPasses;
using System.Collections.Generic;
using System.Linq;

namespace ZooPassTests
{
    [TestClass]
    public class ZooPassesTests
    {

        //create some singlepass holder
        SinglePassHolder singlePassHolder1 = new SinglePassHolder
        {
            CustomerId = Guid.NewGuid(),
            FirstName = "Bob",
            LastName = "Smith",
            Email = "bob@bob.com",
            PassType = "Single",
            IsPassActive = false
        };

        SinglePassHolder singlePassHolder2 = new SinglePassHolder
        {
            CustomerId = Guid.NewGuid(),
            FirstName = "Bill",
            LastName = "Watson",
            Email = "bill@bill.com",
            PassType = "Single",
            IsPassActive = true
        };

        SinglePassHolder singlePassHolder3 = new SinglePassHolder
        {
            CustomerId = Guid.NewGuid(),
            FirstName = "Jenny",
            LastName = "Adams",
            Email = "Jenny@Jenny.com",
            PassType = "Single",
            IsPassActive = true
        };

        SinglePassHolder singlePassHolder4 = new SinglePassHolder
        {
            CustomerId = Guid.NewGuid(),
            FirstName = "Pepe",
            LastName = "Lopez",
            Email = "Pepe@Pepe.com",
            PassType = "Single",
            IsPassActive = true
        };
        SinglePassHolder singlePassHolder5 = new SinglePassHolder
        {
            CustomerId = Guid.NewGuid(),
            FirstName = "Randy",
            LastName = "Orton",
            Email = "Orton@Orton.com",
            PassType = "Single",
            IsPassActive = false
        };
        //build a list of single pass holders  
        List<SinglePassHolder> singlePassHolders = new List<SinglePassHolder>();
       


        [TestMethod]
        public void SinglePassesMoreThan1()
        {
            singlePassHolders.Add(singlePassHolder1);
            singlePassHolders.Add(singlePassHolder2);
            singlePassHolders.Add(singlePassHolder3);
            singlePassHolders.Add(singlePassHolder4);
            singlePassHolders.Add(singlePassHolder5);


            List<SinglePassHolder> singleCustomersHavingActivePasses = singlePassHolders.Where(a => a.IsPassActive == false).OrderBy(a => a.LastName).ToList();
            Assert.IsTrue(singleCustomersHavingActivePasses.Count() > 1);
          
        }

        [TestMethod]
        public void SinglePassesListIsNotNullForActivePasses()
        {
            singlePassHolders.Add(singlePassHolder1);
            singlePassHolders.Add(singlePassHolder2);
            singlePassHolders.Add(singlePassHolder3);
            singlePassHolders.Add(singlePassHolder4);
            singlePassHolders.Add(singlePassHolder5);


            List<SinglePassHolder> singleCustomersHavingActivePasses = singlePassHolders.Where(a => a.IsPassActive == false).OrderBy(a => a.LastName).ToList();
            Assert.IsNotNull(singleCustomersHavingActivePasses);
         
        }

        [TestMethod]
        public void SinglePassesAllShouldBeActive()
        {
            singlePassHolders.Add(singlePassHolder1);
            singlePassHolders.Add(singlePassHolder2);
            singlePassHolders.Add(singlePassHolder3);
            singlePassHolders.Add(singlePassHolder4);
            singlePassHolders.Add(singlePassHolder5);

            List<SinglePassHolder> singleCustomersHavingActivePasses = singlePassHolders.Where(a => a.IsPassActive == false).OrderBy(a => a.LastName).ToList();
            Assert.IsTrue(singleCustomersHavingActivePasses.Where(a => a.IsPassActive).Count() == 0);
        }
    }
}
