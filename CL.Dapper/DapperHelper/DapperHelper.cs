﻿using System.Data;
using System.Configuration;
using System.Data.SqlClient;

namespace CL.Dapper.DapperHelper
{
    /// <summary>
    /// Dapper 帮助类。
    /// </summary>
    public class DapperHelper
    {
        // 数据库连接名
        private static string _connection = string.Empty;

        // 获取连接名        
        private static string Connection
        {
            get { return _connection; }
            set { _connection = value; }
        }

        public DapperHelper() => _connection = AppSettings.connectionStrings.Default;

        // 返回连接实例 
        private static IDbConnection dbConnection = null;

        /// 静态变量保存类的实例        
        private static DapperHelper? uniqueInstance;

        /// 定义一个标识确保线程同步        
        private static readonly object locker = new object();

        /// <summary>
        /// 获取实例，这里为单例模式，保证只存在一个实例
        /// </summary>
        /// <returns></returns>
        public static DapperHelper GetInstance()
        {
            // 双重锁定实现单例模式，在外层加个判空条件主要是为了减少加锁、释放锁的不必要的损耗
            if (uniqueInstance == null)
            {
                lock (locker)
                {
                    if (uniqueInstance == null)
                    {
                        uniqueInstance = new DapperHelper();
                    }
                }
            }
            return uniqueInstance;
        }

        /// <summary>
        /// 创建数据库连接对象并打开链接
        /// </summary>
        /// <returns></returns>
        public static IDbConnection OpenCurrentDbConnection()
        {
            if (dbConnection == null)
            {
                dbConnection = new SqlConnection(Connection);
            }
            //判断连接状态
            if (dbConnection.State == ConnectionState.Closed)
            {
                dbConnection.Open();
            }
            return dbConnection;
        }
    }
}
