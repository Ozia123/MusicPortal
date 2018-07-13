using AutoMapper;
using MusicPortal.DAL.Interfaces;
using MusicPortal.ViewModels.MappingConfiguration;
using MusicPortal.WebForms.Factories;
using System;
using System.Web.UI;

namespace MusicPortal.WebForms.Forms.Base {
    public abstract class AppPage : Page {
        protected IUnitOfWork Database { get; private set; }
        protected IMapper Mapper { get; private set; }

        protected virtual void Page_Load(object sender, EventArgs e) {
            Database = UnitOfWorkFactory.GetUnitOfWork();
            Mapper = MappingConfiguration.GetConfiguration().CreateMapper();
        }
    }
}