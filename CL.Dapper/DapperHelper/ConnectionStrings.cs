namespace CL.Dapper.DapperHelper
{
    /// <summary>
    /// 数据库配置对象实体，该实体的命名及属性名称要与appsetting.json里的属性一致。
    /// </summary>
    public class ConnectionStrings
    {
        public string? Default { get; set; }
    }
}
