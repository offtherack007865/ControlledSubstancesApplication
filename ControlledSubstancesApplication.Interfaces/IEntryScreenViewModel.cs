using System.Collections.Generic;
using ControlledSubstancesApplication.Interfaces;
using PagedList;

namespace ControlledSubstancesApplication.Interfaces
{
    public interface IEntryScreenViewModel
    {
        List<IEntry> AllEntriesList { get; set; }
        int CurrentPage { get; set; }
        string DrugName { get; set; }
        List<IEntry> EntriesForDisplay { get; set; }
        int EntriesPerPage { get; set; }
        IEntry Entry { get; set; }
        int LotId { get; set; }
        string LotNumber { get; set; }
        bool MrnIsValid { get; set; }
        IPagedList<IEntry> PagedEntriesList { get; set; }
        int PagingLowerLimit { get; set; }
        int PagingUpperLimit { get; set; }
        string PatientDob { get; set; }
        string PatientName { get; set; }
        ISite Site { get; set; }
        IUserList Users { get; set; }
    }
}