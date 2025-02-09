using Npgsql;

namespace URLS.Data.Helpers;

public static class ExceptionHelper
{
    public static bool IsUniqueConstraintViolation(Exception ex, string constraintName)
    {
        if (ex.InnerException is PostgresException pex)
        {
            return pex.SqlState == "23505" && pex.ConstraintName.Equals(constraintName);
        }
        return false;
    }
}
