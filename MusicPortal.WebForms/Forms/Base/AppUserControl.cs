using AutoMapper;
using MusicPortal.DAL.Interfaces;
using MusicPortal.ViewModels.MappingConfiguration;
using MusicPortal.WebForms.Factories;
using System;
using System.Web.UI;

namespace MusicPortal.WebForms.Forms.Base {
    public abstract class AppUserControl : UserControl {
        protected IUnitOfWork Database { get; private set; }
        protected IMapper Mapper { get; private set; }

        protected virtual void Page_Load(object sender = null, EventArgs e = null) {
            Database = UnitOfWorkFactory.GetUnitOfWork();
            Mapper = MappingConfiguration.GetConfiguration().CreateMapper();
        }
    }
}