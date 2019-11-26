# LovaMvc

lovamvc是一个通用的基础项目。当开发新的非分布式项目时，我们总是从头开始的去建设基础模块（例如：后台的ui选择，用户、角色、权限、日志、图片等），总是在做重复的工作。
因此将共同的模块作为一个基础的项目，以便以后期项目的构建。只需要Crtl+C即可。

![图片](https://github.com/lovachen/LovaMvc/blob/master/1574754461(1).jpg)


## 功能
  项目主要实现的是基础部分，含有OSS管理、系统管理、菜单等基础功能。
  
* 此项目采用  asp.net core 3.0 + ef core + mysql8 + ace admin(ui) 构建

1. OSS管理  
   * Bucket 
   * 图库
2. 系统管理  
   * 用户列表 
   * 角色管理
   * 操作日志
   * 操作日志业务说明
   * 系统参数设置
   * 错误日志
   * 任务调度
   
## 用法
  安装myslq8数据库
  
    * 项目中含有数据的sql文件 lovamvc.sql，执行后即可创建数据库，含有默认的后台管理远用户（账号：admin,密码：123456）
    
  安装.net core 3运行环境
  
  默认后台登录地址：/admin/login
