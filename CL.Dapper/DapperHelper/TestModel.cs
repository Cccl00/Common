namespace CL.Dapper.DapperHelper
{
    public class TestModel
    {
        /// <summary>
        /// 参考号
        /// </summary>
        public string ReferenceNo { get; set; }

        /// <summary>
        /// 总数量
        /// </summary>
        public virtual int TotalItemCount { get; protected set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public virtual DateTime CreationTime { get; protected set; }
    }
}
