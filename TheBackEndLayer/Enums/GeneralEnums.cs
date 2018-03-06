using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheBackEndLayer.Enums
{
    public enum ApplicantStatus
    {
        Initiated = 1,
        UnderReview = 2,
        Approved = 3,
        Rejected = 4
    }

    public enum RejectionReason
    {
        ShareHolderRejected = 1,
        FakeEmail = 2,
        DirtyRecord = 3,
        Other = 4
    }

    public enum Gender
    {
        Male = 1,
        Female = 2
    }

    public enum TeeTimeStatus
    {
        Open = 1,
        Full = 2,
        Closed = 3,
        Tentative = 4
    }

    public enum ReservationStatus
    {
        Accepted = 1,
        NotAccepted = 2,
        Rejected = 3
    }

    public enum ReservationType
    {
        Normal = 1,
        Standing = 2,
    }

    public enum Role
    {
        Administrator = 1,
        Clerk = 2,
        GolfDayClerk = 3,
        BoardOfDirector = 4
    }
}
