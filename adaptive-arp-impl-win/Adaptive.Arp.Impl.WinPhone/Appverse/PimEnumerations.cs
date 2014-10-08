using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adaptive.Arp.Impl.WinPhone.Appverse
{
    public enum RelationshipType
    {
        None,
        Brother,
        Sister,
        Parent,
        Child,
        Friend,
        Partner,
        Relative,
        Spouse
    }

    public enum NumberType
    {
        Other,
        Mobile,
        FixedLine,
        Work,
        HomeFax,
        WorkFax,
        Pager
    }

    public enum DispositionType
    {
        Other,
        Personal,
        Work,
        HomeOffice
    }

    public enum CalendarType
    {
        Other,
        Birthday,
        CalDAV,
        Exchange,
        IMap,
        Local,
        Subscription
    }

    public enum AttendeeStatus
    {
        NeedsAction,
        Accepted,
        Declined,
        Tentative
    }

    public enum AlarmAction
    {
        Display,
        Email,
        Sound
    }

    public enum RecurrenceType
    {
        Weekly,
        Fortnightly,
        FourWeekly,
        Montly,
        Yearly
    }

    public enum ContactQueryColumn
    {
        ID,
        Name,
        Phone
    }

    public enum ContactQueryCondition
    {
        Equals,
        StartsWith,
        EndsWith,
        Contains,
        Available
    }

}
