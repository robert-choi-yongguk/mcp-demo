namespace closet_demo.Helpers;

public static class ClosetPathHelper
{
    public static string GetWorkroomPath(string workroomId) 
        => $"{Constants.ServiceHost}/room/{workroomId}";
}