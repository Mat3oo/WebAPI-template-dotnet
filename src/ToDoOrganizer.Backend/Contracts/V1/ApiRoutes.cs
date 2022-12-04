namespace ToDoOrganizer.Backend.Contracts.V1
{
    public static class ApiRoutes
    {
        public const string Root = "/api";
        public const string Base = Root;
        public static class Projects{
            public const string Get = Base + "/projects/{id}";
            public const string GetAll = Base + "/projects";
            public const string Create = Base + "/projects";
            public const string Update = Base + "/projects/{id}";
            public const string Delete = Base + "/projects/{id}";
            public const string GetAllOData = Base + "/odata/projects";
        }
    }
}