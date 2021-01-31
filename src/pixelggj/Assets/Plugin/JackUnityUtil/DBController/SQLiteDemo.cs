using System;
using System.Collections.Generic;
using SQLite;

namespace JackUtil {

    [Table("Account")]
    public class AccountTable {

        [PrimaryKey, AutoIncrement]
        [Column("id")]
        public long id { get; set; }

        [Column("username")]
        public string username { get; set; }

        [Column("password")]
        public string password { get; set; }

    }

    public class SQLiteDemo {

        public SQLiteConnection connection;

        public SQLiteDemo() {

            // 创建数据库
            connection = new SQLiteConnection("Jack.db");

            // 创建表
            connection.CreateTable<AccountTable>();

            // 插入数据
            // int id = connection.Insert(new AccountTable() {
            //     username = "jack",
            //     password = "rsdyxjh"
            // });

            connection.Update(new AccountTable() { id = 10, username = "n"}, typeof(AccountTable));

            // 查询数据
            string username = "jack";
            List<AccountTable> accounts = connection.Query<AccountTable>("select * from Account where username = ?", username);
            foreach (AccountTable tb in accounts) {
                DebugHelper.Log(tb.id);
                DebugHelper.Log(tb.username);
                DebugHelper.Log(tb.password);
                // connection.Delete<AccountTable>(tb.id);
            }

            // 删除数据

            connection.Close();

        }

    }
}