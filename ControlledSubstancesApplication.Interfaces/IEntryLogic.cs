

using System.Web.Mvc;

namespace ControlledSubstancesApplication.Interfaces
{
    public interface IEntryLogic
    {
        IEntryScreenViewModel EntryScreen { get; set; }
        void GetEntries(FormCollection form, IEntryScreenViewModel EntryScreen);

        void CreatePagedList();
        void PopulateValues();
        void Save();
    }
}