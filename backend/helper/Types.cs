using System.ComponentModel;

namespace backend.helper;
public enum WashOrderStatus
{
    Pending = 0,
    Completed = 1,
    Cancelled = 2, 
}

public enum Role
{
    [Description("User")]
    User = 0,
    [Description("Washer")]
    Washer = 1,
    [Description("Admin")]
    Admin = 2
}



public enum NotificationType
{
    General = 0,
    Crucial = 1
}

public static class RoleExtensions{
    public static string GetRole(this Role role){
        switch(role){
            case Role.Admin:
                return "Admin";
            case Role.User:
                return "User";
            case Role.Washer:
                return "Washer";
            default : return "Guest";
        }
    }
}




