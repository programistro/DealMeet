namespace DealMeet.Core;

public class FilterUsersRequest
{
    public Guid UserId { get; set; }
    public int PageSize { get; set; } = 10;
    public int PageNumber { get; set; } = 1;
}
