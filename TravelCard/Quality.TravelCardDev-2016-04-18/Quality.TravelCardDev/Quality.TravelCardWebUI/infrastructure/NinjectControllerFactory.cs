using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Ninject;
using Ninject.Modules;

using System.Configuration;

namespace Quality.WebUI.Infrastructure
{
    public class NinjectControllerFactory : DefaultControllerFactory
    {
        private IKernel _kernel = new StandardKernel(new NinjectBindingDefinitions());
        protected override IController GetControllerInstance(System.Web.Routing.RequestContext requestContext, Type controllerType)
        {
            if (controllerType == null)
                return null;
            return (IController)_kernel.Get(controllerType);
        }

        private class NinjectBindingDefinitions : NinjectModule
        {
            public override void Load()
            {
                Bind(typeof(Ideal.DomainModel.Abstract.IPartsRepository<>))
                    .To(typeof(Ideal.DomainModel.Repositories.PartRepository));
                Bind(typeof(Ideal.DomainModel.Abstract.IComponentsRepository<>))
                   .To(typeof(Ideal.DomainModel.Repositories.ComponentRepository));
                Bind(typeof(TravelCard.DomainModel.Abstract.IPartSetUpRepository))
                .To(typeof(TravelCard.DomainModel.Repositories.PartSetUpRepository))
                .WithConstructorArgument("connectionString",ConfigurationManager.ConnectionStrings["Quality_ConnString"].ConnectionString);
                Bind(typeof(TravelCard.DomainModel.Abstract.IPartCategoryRepository))
               .To(typeof(TravelCard.DomainModel.Repositories.PartCategoryRepository))
               .WithConstructorArgument("connectionString", ConfigurationManager.ConnectionStrings["Quality_ConnString"].ConnectionString);

                Bind(typeof(TravelCard.DomainModel.Abstract.IAdditionalProcessingRepository))
              .To(typeof(TravelCard.DomainModel.Repositories.AdditionalProcessingRepository))
              .WithConstructorArgument("connectionString", ConfigurationManager.ConnectionStrings["Quality_ConnString"].ConnectionString);

                Bind(typeof(TravelCard.DomainModel.Abstract.IPartSpecificationRepository))
                             .To(typeof(TravelCard.DomainModel.Repositories.PartSpecificationRepository))
                             .WithConstructorArgument("connectionString", ConfigurationManager.ConnectionStrings["Quality_ConnString"].ConnectionString);

                Bind(typeof(TravelCard.DomainModel.Abstract.IMeasurementMethodRepository))
                               .To(typeof(TravelCard.DomainModel.Repositories.MeasurementMethodRepository))
                               .WithConstructorArgument("connectionString", ConfigurationManager.ConnectionStrings["Quality_ConnString"].ConnectionString);

                Bind(typeof(TravelCard.DomainModel.Abstract.IPlantRepository))
                            .To(typeof(TravelCard.DomainModel.Repositories.PlantRepository))
                            .WithConstructorArgument("connectionString", ConfigurationManager.ConnectionStrings["Quality_ConnString"].ConnectionString);

                Bind(typeof(TravelCard.DomainModel.Abstract.IUserSettingRepository))
                           .To(typeof(TravelCard.DomainModel.Repositories.UserSettingRepository))
                           .WithConstructorArgument("connectionString", ConfigurationManager.ConnectionStrings["Quality_ConnString"].ConnectionString);

                Bind(typeof(TravelCard.DomainModel.Abstract.ILanguageRepository))
                        .To(typeof(TravelCard.DomainModel.Repositories.LanguageRepository))
                        .WithConstructorArgument("connectionString", ConfigurationManager.ConnectionStrings["Quality_ConnString"].ConnectionString);


                Bind(typeof(TravelCard.DomainModel.Abstract.IMeasurementUnitRepository))
                      .To(typeof(TravelCard.DomainModel.Repositories.MeasurementUnitRepository))
                      .WithConstructorArgument("connectionString", ConfigurationManager.ConnectionStrings["Quality_ConnString"].ConnectionString);

                Bind(typeof(TravelCard.DomainModel.Abstract.IFrequencyRepository))
                    .To(typeof(TravelCard.DomainModel.Repositories.FrequencyRepository))
                    .WithConstructorArgument("connectionString", ConfigurationManager.ConnectionStrings["Quality_ConnString"].ConnectionString);

                Bind(typeof(TravelCard.DomainModel.Abstract.ITravelCardRepository))
                 .To(typeof(TravelCard.DomainModel.Repositories.TravelCardRepository))
                 .WithConstructorArgument("connectionString", ConfigurationManager.ConnectionStrings["Quality_ConnString"].ConnectionString);

                Bind(typeof(TravelCard.DomainModel.Abstract.ITCBarCodeRepository))
               .To(typeof(TravelCard.DomainModel.Repositories.TCBarCodeRepository))
               .WithConstructorArgument("connectionString", ConfigurationManager.ConnectionStrings["Quality_ConnString"].ConnectionString);

                Bind(typeof(TravelCard.DomainModel.Abstract.IPartSpecificationSequenceRepository))
             .To(typeof(TravelCard.DomainModel.Repositories.PartSpecificationSequenceRepository))
             .WithConstructorArgument("connectionString", ConfigurationManager.ConnectionStrings["Quality_ConnString"].ConnectionString);

               

            }
        }
    }
}