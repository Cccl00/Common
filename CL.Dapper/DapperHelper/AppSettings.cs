namespace CL.Dapper.DapperHelper
{
    /// <summary>
    /// 应用设置类
    /// 总类：对应json文件，确定json模块与对象
    /// </summary>
    public static class AppSettings
    {
        public static ConnectionStrings connectionStrings { get; set; }

        public static void Init(IConfiguration configuration)
        {
            // 将Setting模块绑定到Json模块的Setting类
            connectionStrings = new ConnectionStrings();
            // 创建一个ConnectionStrings类型的实例，并将其绑定到configuration中
            configuration.Bind("ConnectionStrings", connectionStrings);
        }

    }

}
