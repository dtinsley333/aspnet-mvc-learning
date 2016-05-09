using System;
using System.Collections.Generic;
using Quality.WebUI.Controllers;
using System.Configuration;
using Quality.ViewModels;
using System.Linq;
using System.Data;
using System.Web;
using System.Web.Mvc;
using System.Collections;
using TravelCard.DomainModel.Repositories;
using TravelCard.DomainModel.Abstract;
using TravelCard.DomainModel.Entities;
using Quality.BarCodeHelpers;
using System.Globalization;
using System.Threading;
using Quality.WebUI.Controllers;
using Quality.ViewModels;

namespace Quality.Controllers
{
    public class BarCodeController : BaseController
    {

        IPartSetUpRepository _partsetupRepository;
        IUserSettingRepository _usersettingsRepository;
        ILanguageRepository _languageRepository;
        ITravelCardRepository _travelcardRepository;

        public BarCodeController(IPartSetUpRepository partsetupRepository_, IUserSettingRepository usersettingsRepository_, ILanguageRepository languageRepository_, ITravelCardRepository travelCardRepository_)
        {
            _partsetupRepository = partsetupRepository_;
            _usersettingsRepository = usersettingsRepository_;
            _languageRepository = languageRepository_;
            _travelcardRepository = travelCardRepository_;
        }


        public ActionResult BarCodeDisplay(string id, bool showText = true, int thickness = 3, int height = 70)
        {

            string barcodesavepath = ConfigurationManager.AppSettings["BarCodeSavePath"];
            string filepath = barcodesavepath;
            var barcode = new Code39BarCode()
            {
                BarCodeText = id,
                Height = height,
                ShowBarCodeText = showText
            };

            if (thickness == 2)
                barcode.BarCodeWeight = BarCodeWeight.Medium;
            else if (thickness == 3)
                barcode.BarCodeWeight = BarCodeWeight.Large;
      
    
        ImageResult result= this.Image(barcode.Generate(), "image/gif",id,filepath);
        //added this hack because the users in mexico were having the travel card display prior to the bar codes being written.
           
         return result;
          
        }


        public ActionResult TCContinuationBarCodeDisplay(TravelCardPrintViewModel viewModel, string barcodetext,int? opcode, int thispartsetupid, bool isdraft, bool iscontinuationcard, string id, bool showText = true, int thickness = 3, int height = 70)
        {
            //first save the travel card record, need the travel card id from the database to build the barcode.
            int travelcardid = SaveContinuationTravelCards(viewModel, barcodetext,isdraft, opcode, thispartsetupid, iscontinuationcard);
            string travelcardidvalue = travelcardid.ToString();

            //mow save the bar code with the 
            string barcodesavepath = ConfigurationManager.AppSettings["BarCodeSavePath"];
            string filepath = barcodesavepath;
            var barcode = new Code39BarCode()
            {
                BarCodeText = barcodetext,
                Height = height,
                ShowBarCodeText = showText
            };

            if (thickness == 2)
                barcode.BarCodeWeight = BarCodeWeight.Medium;
            else if (thickness == 3)
                barcode.BarCodeWeight = BarCodeWeight.Large;

            id = id + "-" + travelcardid;
            ImageResult result = this.Image(barcode.Generate(), "image/gif", id, filepath);
            //added this hack because the users in mexico were having the travel card display prior to the bar codes being written.


            string BarcodeFilePath = filepath + id + ".gif";


            Session["tcbarcodepath"] = BarcodeFilePath;
            string thissession = Convert.ToString(Session["tcbarcodepath"]);
            Session["barcodetext"] = id;
            return result;

        }







  
        public ActionResult TCBarCodeDisplay(TravelCardPrintViewModel viewModel,int? opcode, int thispartsetupid,bool isdraft,bool iscontinuationcard,string id, bool showText = true, int thickness = 3, int height = 70 )
        {
             //first save the travel card record, need the travel card id from the database to build the barcode.
            int travelcardid = SaveTravelCards(viewModel,isdraft, opcode, thispartsetupid, iscontinuationcard);
            string travelcardidvalue = travelcardid.ToString();
           
            //now save the bar code with the 
            string barcodesavepath = ConfigurationManager.AppSettings["BarCodeSavePath"];
            string filepath = barcodesavepath;
            var barcode = new Code39BarCode()
            {
                BarCodeText = id + "-"+ travelcardid,
                Height = height,
                ShowBarCodeText = showText
            };

            if (thickness == 2)
                barcode.BarCodeWeight = BarCodeWeight.Medium;
            else if (thickness == 3)
                barcode.BarCodeWeight = BarCodeWeight.Large;

            id = id + "-" + travelcardid;
            ImageResult result = this.Image(barcode.Generate(), "image/gif", id, filepath);
            //added this hack because the users in mexico were having the travel card display prior to the bar codes being written.

          
                string BarcodeFilePath = filepath + id+ ".gif";


                Session["tcbarcodepath"] = BarcodeFilePath;
                string thissession = Convert.ToString(Session["tcbarcodepath"]);
                Session["barcodetext"] = id;
            return result;

        }


        public int SaveTravelCards(TravelCardPrintViewModel viewModel,
            bool isdraft, int? operationcode, int partsetupid, bool iscontinuationcard)
        {

            GetUserInfo();
            string user = username;
            string language = ResolveCulture().Name;

            viewModel.TCLanguage = _languageRepository.Language.FirstOrDefault(a => a.LanguageCode == language.Trim());
            viewModel.UserSetting = _usersettingsRepository.UserSetting.FirstOrDefault(a => a.UserName == username);
            viewModel.PartSetUp = _partsetupRepository.PartSetUp.FirstOrDefault(a => a.PartSetUpID == partsetupid);
            //TODO: Find out why I had to declare a new TravelCard since it was included in the view model.
            TravelCard.DomainModel.Entities.TravelCard travelcard_ = new TravelCard.DomainModel.Entities.TravelCard();
      
            //save the travel card record

            int travelcardID = 0;
            travelcard_.PartSetUpID = viewModel.PartSetUp.PartSetUpID;
            travelcard_.IsContinuationCard = iscontinuationcard;
            travelcard_.LanguageID = Convert.ToInt16(viewModel.TCLanguage.LanguageID);
            travelcard_.IsDraft = isdraft;
            travelcard_.OperationCode = Convert.ToInt16(operationcode);
            travelcard_.PrintDate = DateTime.Now;
            travelcard_.PrintedBy = username;
            travelcard_.PrintLocation = viewModel.UserSetting.Plant.PlantName;
            travelcard_.Notes = "";
            travelcardID = _travelcardRepository.Insert(travelcard_);
        

         
              //save the bar code text;
              string plantnum = viewModel.UserSetting.Plant.PlantCode;
              string partsetup = viewModel.PartSetUp.PartSetUpID.ToString();
              string tcid = travelcardID.ToString();
              string barcodeid = plantnum + "-" + partsetup + "-" + tcid;
              viewModel.TravelCard = _travelcardRepository.TravelCard.FirstOrDefault(a => a.TCID == travelcardID);
              viewModel.TravelCard.TCBarCodeText = barcodeid;
              _travelcardRepository.Update(viewModel.TravelCard);



        return travelcardID;
        }


        public int SaveContinuationTravelCards(TravelCardPrintViewModel viewModel, string barcodetext,
                   bool isdraft, int? operationcode, int partsetupid, bool iscontinuationcard)
        {

            GetUserInfo();
            string user = username;
            string language = ResolveCulture().Name;
     
            viewModel.TCLanguage = _languageRepository.Language.FirstOrDefault(a => a.LanguageCode == language.Trim());
            viewModel.UserSetting = _usersettingsRepository.UserSetting.FirstOrDefault(a => a.UserName == username);
            viewModel.PartSetUp = _partsetupRepository.PartSetUp.FirstOrDefault(a => a.PartSetUpID == partsetupid);
            //TODO: Find out why I had to declare a new TravelCard since it was included in the view model.
            TravelCard.DomainModel.Entities.TravelCard travelcard_ = new TravelCard.DomainModel.Entities.TravelCard();

            //save the travel card record

            int travelcardID = 0;
            travelcard_.PartSetUpID = viewModel.PartSetUp.PartSetUpID;
            travelcard_.IsContinuationCard = iscontinuationcard;
            travelcard_.LanguageID = Convert.ToInt16(viewModel.TCLanguage.LanguageID);
            travelcard_.IsDraft = isdraft;
            travelcard_.OperationCode = Convert.ToInt16(operationcode);
            travelcard_.PrintDate = DateTime.Now;
            travelcard_.PrintedBy = username;
            travelcard_.PrintLocation = viewModel.UserSetting.Plant.PlantName;
            travelcard_.Notes = "";
            travelcardID = _travelcardRepository.Insert(travelcard_);

            //save the bar code text;
         
            string barcodeid = barcodetext;
            viewModel.TravelCard = _travelcardRepository.TravelCard.FirstOrDefault(a => a.TCID == travelcardID);
            viewModel.TravelCard.TCBarCodeText = barcodeid;
            _travelcardRepository.Update(viewModel.TravelCard);



            return travelcardID;
        }












        //public ActionResult BarCodeDisplay(string id, bool showText = true, int thickness = 1, int height = 60)
        //{
        //     ImageResult image = BarCodeGenerate(id, showText, thickness, height);

        //    BarCodeViewModel viewModel = new BarCodeViewModel
            
        //    {
        //   BarcodeFilePath=image.FilePath
            
        //    };

        //    return View(viewModel);
        //    }

    }
}
