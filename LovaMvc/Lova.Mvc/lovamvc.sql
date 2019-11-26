CREATE DATABASE  IF NOT EXISTS `lovamvc` /*!40100 DEFAULT CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci */ /*!80016 DEFAULT ENCRYPTION='N' */;
USE `lovamvc`;
-- MySQL dump 10.13  Distrib 8.0.18, for Win64 (x86_64)
--
-- Host: 182.48.115.232    Database: lovamvc
-- ------------------------------------------------------
-- Server version	8.0.18

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!50503 SET NAMES utf8 */;
/*!40103 SET @OLD_TIME_ZONE=@@TIME_ZONE */;
/*!40103 SET TIME_ZONE='+00:00' */;
/*!40014 SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;

--
-- Table structure for table `bucket`
--

DROP TABLE IF EXISTS `bucket`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `bucket` (
  `id` varchar(36) NOT NULL,
  `name` varchar(45) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL,
  `is_compress` tinyint(1) NOT NULL DEFAULT '0',
  `description` varchar(450) CHARACTER SET utf8 COLLATE utf8_general_ci DEFAULT NULL,
  `creator` varchar(36) NOT NULL,
  `creation_time` datetime NOT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `bucket`
--

LOCK TABLES `bucket` WRITE;
/*!40000 ALTER TABLE `bucket` DISABLE KEYS */;
INSERT INTO `bucket` VALUES ('c8936e01-914a-4d40-8aac-3337f3465c71','avater',0,NULL,'e12d5344-0a9f-11ea-93cb-000c29f1800f','2019-11-22 15:43:27');
/*!40000 ALTER TABLE `bucket` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `bucket_cut`
--

DROP TABLE IF EXISTS `bucket_cut`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `bucket_cut` (
  `id` varchar(36) NOT NULL,
  `bucket_id` varchar(36) NOT NULL,
  `value` varchar(145) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL,
  `creator` varchar(36) NOT NULL,
  `creation_time` datetime NOT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `bucket_cut`
--

LOCK TABLES `bucket_cut` WRITE;
/*!40000 ALTER TABLE `bucket_cut` DISABLE KEYS */;
INSERT INTO `bucket_cut` VALUES ('3da86e01-3d3f-429e-bb62-9c97e5c06e86','c8936e01-914a-4d40-8aac-3337f3465c71','m_fill,w_110,h_110','e12d5344-0a9f-11ea-93cb-000c29f1800f','2019-11-26 15:03:37'),('7aa76e01-1551-4dae-a535-994d5df93c69','c8936e01-914a-4d40-8aac-3337f3465c71','m_w,w_300','e12d5344-0a9f-11ea-93cb-000c29f1800f','2019-11-26 11:30:42');
/*!40000 ALTER TABLE `bucket_cut` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `bucket_image`
--

DROP TABLE IF EXISTS `bucket_image`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `bucket_image` (
  `id` varchar(36) NOT NULL,
  `visiturl` varchar(1450) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL,
  `io_path` varchar(1450) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL,
  `ext_name` varchar(20) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL,
  `sha1` varchar(512) CHARACTER SET utf8 COLLATE utf8_general_ci DEFAULT NULL,
  `bucket_id` varchar(36) NOT NULL,
  `creation_time` datetime NOT NULL,
  `width` int(11) NOT NULL DEFAULT '0',
  `height` int(11) NOT NULL DEFAULT '0',
  `length` bigint(19) NOT NULL DEFAULT '0',
  PRIMARY KEY (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `bucket_image`
--

LOCK TABLES `bucket_image` WRITE;
/*!40000 ALTER TABLE `bucket_image` DISABLE KEYS */;
INSERT INTO `bucket_image` VALUES ('57a86e01-b19d-4159-afd3-0eef57b29d32','/oss/imagecn/avater/239/3226404847/57a86e01-cf8c-4d65-a649-e5dbd773872d.jpg','MediaItems\\avater\\239\\3226404847\\57a86e01-cf8c-4d65-a649-e5dbd773872d.jpg','.JPG','C5BC2ACDB76EEDEA7FD8636D8215F86A82292480','c8936e01-914a-4d40-8aac-3337f3465c71','2019-11-26 15:32:25',950,621,39246);
/*!40000 ALTER TABLE `bucket_image` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `quarzt_schedule`
--

DROP TABLE IF EXISTS `quarzt_schedule`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `quarzt_schedule` (
  `id` varchar(36) NOT NULL,
  `job_group` varchar(256) NOT NULL,
  `job_name` varchar(256) NOT NULL,
  `run_status` int(11) NOT NULL,
  `cron_express` varchar(64) DEFAULT NULL,
  `start_run_time` datetime DEFAULT NULL,
  `end_run_time` datetime DEFAULT NULL,
  `nex_run_time` datetime DEFAULT NULL,
  `data_status` int(11) DEFAULT NULL,
  `job_run_time` datetime DEFAULT NULL,
  `task_description` varchar(450) CHARACTER SET utf8 COLLATE utf8_general_ci DEFAULT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `quarzt_schedule`
--

LOCK TABLES `quarzt_schedule` WRITE;
/*!40000 ALTER TABLE `quarzt_schedule` DISABLE KEYS */;
/*!40000 ALTER TABLE `quarzt_schedule` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `sys_activitylog`
--

DROP TABLE IF EXISTS `sys_activitylog`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `sys_activitylog` (
  `id` varchar(36) NOT NULL,
  `method` varchar(45) CHARACTER SET utf8 COLLATE utf8_general_ci DEFAULT NULL,
  `entity_name` varchar(145) CHARACTER SET utf8 COLLATE utf8_general_ci DEFAULT NULL,
  `primary_key` varchar(64) CHARACTER SET utf8 COLLATE utf8_general_ci DEFAULT NULL,
  `creation_time` datetime NOT NULL,
  `creator` varchar(36) DEFAULT NULL,
  `oldvalue` text,
  `newvalue` text,
  `comment` varchar(145) CHARACTER SET utf8 COLLATE utf8_general_ci DEFAULT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci COMMENT='用户操作日志';
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `sys_activitylog`
--

LOCK TABLES `sys_activitylog` WRITE;
/*!40000 ALTER TABLE `sys_activitylog` DISABLE KEYS */;
/*!40000 ALTER TABLE `sys_activitylog` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `sys_activitylog_comment`
--

DROP TABLE IF EXISTS `sys_activitylog_comment`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `sys_activitylog_comment` (
  `entity_name` varchar(100) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL,
  `comment` varchar(145) DEFAULT NULL,
  PRIMARY KEY (`entity_name`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `sys_activitylog_comment`
--

LOCK TABLES `sys_activitylog_comment` WRITE;
/*!40000 ALTER TABLE `sys_activitylog_comment` DISABLE KEYS */;
INSERT INTO `sys_activitylog_comment` VALUES ('bucket','图片存储桶'),('bucket_cut','图片剪裁配置'),('bucket_image','图片记录'),('quarzt_schedule','定时任务'),('sys_activitylog','操作日志'),('sys_activitylog_comment',NULL),('sys_category','功能、菜单表'),('sys_nlog','错误日志'),('sys_permission','角色权限表'),('sys_role','系统角色表'),('sys_setting','参数设置表'),('sys_user','系统用户表'),('sys_user_jwt','系统用户token'),('sys_user_login','系统用户登录记录表'),('sys_user_role','系统用户角色关联');
/*!40000 ALTER TABLE `sys_activitylog_comment` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `sys_category`
--

DROP TABLE IF EXISTS `sys_category`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `sys_category` (
  `id` varchar(36) NOT NULL,
  `name` varchar(45) NOT NULL,
  `is_menu` tinyint(1) NOT NULL DEFAULT '0',
  `uid` varchar(128) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL,
  `code` varchar(128) CHARACTER SET utf8 COLLATE utf8_general_ci DEFAULT NULL,
  `father_code` varchar(128) CHARACTER SET utf8 COLLATE utf8_general_ci DEFAULT NULL,
  `route_template` varchar(128) CHARACTER SET utf8 COLLATE utf8_general_ci DEFAULT NULL,
  `route_name` varchar(128) CHARACTER SET utf8 COLLATE utf8_general_ci DEFAULT NULL,
  `icon_class` varchar(45) CHARACTER SET utf8 COLLATE utf8_general_ci DEFAULT NULL,
  `sort` int(11) NOT NULL DEFAULT '0',
  `target` varchar(2) CHARACTER SET utf8 COLLATE utf8_general_ci DEFAULT NULL,
  `controller` varchar(128) CHARACTER SET utf8 COLLATE utf8_general_ci DEFAULT NULL,
  `action` varchar(128) CHARACTER SET utf8 COLLATE utf8_general_ci DEFAULT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci COMMENT='菜单表';
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `sys_category`
--

LOCK TABLES `sys_category` WRITE;
/*!40000 ALTER TABLE `sys_category` DISABLE KEYS */;
INSERT INTO `sys_category` VALUES ('058a6e01-4238-4b4a-9dac-31293dd60502','菜单管理',0,'SYSMGR.CATEGORY','44d83b3550ae71292cd98f0288017164','54a06f6c1db9874d4b955098fed65b1a','','categoryIndex','menu-icon fa fa-caret-right',8,'0','Category','CategoryIndex'),('0fa26e01-19ce-4520-8c4b-0096f60bf7de','图库',1,'OSSMGR.BUCKET.IMAGE','0e7eb408abec252df18469ebed60683e','734f7e1425a30f2dc064c4e69384a631','','bucketImagesIndex','menu-icon fa fa-caret-right',2,'0','BucketImages','BucketImagesIndex'),('91a76e01-feba-4206-9aba-1d74ede2b6d1','保存水印',0,'SYSMGR.SETTING.MARK','35eab64e68ae91225aad8f5e4428beea','c304e8c73ac8acb38ada78bc4396c442','','saveMark','',2,'0','Setting','SaveMark'),('91a76e01-feba-4308-815f-26e412615548','保存底图',0,'SYSMGR.SETTING.BASEMAB','b30bbd181f7f9715aef33c5413329fdd','c304e8c73ac8acb38ada78bc4396c442','','saveBaseMap','',3,'0','Setting','SaveBaseMap'),('ab7f6e01-91a6-43eb-9e67-0a959095e550','系统管理',1,'SYSMGR','54a06f6c1db9874d4b955098fed65b1a','','','','fa fa-laptop',2,'0','',''),('ab7f6e01-93a6-4157-a4fd-b53bbd6b435c','初始化操作日志业务说明',0,'SYSMGR.ACTIVITYLOGCOMMENT.INIT','65aa50a444d60a5a65b4d5748192c28d','d33c30006b324a48192f80b8088f3f10','','logCommentInit','',1,'0','ActivityLogComment','LogCommentInit'),('ab7f6e01-93a6-4181-8b36-4a16279fe91f','编辑操作日志业务说明',0,'SYSMGR.ACTIVITYLOGCOMMENT.EDIT','dbd6fe0a5fd84db491b03d1acccdc63e','d33c30006b324a48192f80b8088f3f10','','logCommentEdit','',2,'0','ActivityLogComment','LogCommentEdit'),('ab7f6e01-93a6-4333-9211-65333bed7de7','配置用户角色',0,'SYSMGR.USER.USERROLES','9b309aa4b810f3ebf65bd5ac8fb4263f','ca34cbc8c380e8877b07ee5d5cc71a94','','userRoles','',4,'0','SysUser','UserRoles'),('ab7f6e01-93a6-439c-84c0-2750beccb315','修改系统参数',0,'SYSMGR.SETTING.EDIT','a00fd35ff67011097a58e3d70636d973','c304e8c73ac8acb38ada78bc4396c442','','settingEdit','',1,'0','Setting','SettingEdit'),('ab7f6e01-93a6-43e8-b5e7-89f610e09547','新增、修改',0,'SYSMGR.ROLES.EDIT','be9b40f2baa90b61c8b64a3f68a3c686','2eddc7c509391025f0b82c63b85b5096','','roleEdit','',2,'0','SysRole','RoleEdit'),('ab7f6e01-93a6-441c-9ff0-07087655f026','新增',0,'SYSMGR.USER.EDIT','9d681cda55fae7d4bc1ee03321ac0e8a','ca34cbc8c380e8877b07ee5d5cc71a94','','userEdit','',2,'0','SysUser','UserEdit'),('ab7f6e01-93a6-4455-8d27-d1700baa80b9','用户列表',1,'SYSMGR.USERS','ca34cbc8c380e8877b07ee5d5cc71a94','54a06f6c1db9874d4b955098fed65b1a','','userIndex','menu-icon fa fa-caret-right',1,'0','SysUser','UserIndex'),('ab7f6e01-93a6-4479-a90b-ce52002f8f81','错误日志',1,'SYSMGR.NLOG','1712a6bc430e16255b9b9429b83c3bfe','54a06f6c1db9874d4b955098fed65b1a','','nLogIndex','menu-icon fa fa-caret-right',6,'0','NLog','NLogIndex'),('ab7f6e01-93a6-4559-97ec-8699eb8cec30','配置权限',0,'SYSMGR.ROLES.PRM','9539531aa7a516218a1ee457b394aa7b','2eddc7c509391025f0b82c63b85b5096','','rolePrm','',3,'0','SysRole','RolePrm'),('ab7f6e01-93a6-47cf-84c8-d5517b2878b7','系统参数设置',1,'SYSMGR.SETTING','c304e8c73ac8acb38ada78bc4396c442','54a06f6c1db9874d4b955098fed65b1a','','settingIndex','menu-icon fa fa-caret-right',5,'0','Setting','SettingIndex'),('ab7f6e01-93a6-485e-b439-cf823d858475','操作日志业务说明',1,'SYSMGR.ACTIVITYLOGCOMMENT','d33c30006b324a48192f80b8088f3f10','54a06f6c1db9874d4b955098fed65b1a','','logCommentIndex','menu-icon fa fa-caret-right',4,'0','ActivityLogComment','LogCommentIndex'),('ab7f6e01-93a6-48f2-9c46-dfd272bee0b8','删除用户',0,'SYSMGR.USER.DELETE','f40d79851aafcbcb6c7655079f40b589','ca34cbc8c380e8877b07ee5d5cc71a94','','userDelete','',5,'0','SysUser','Delete'),('ab7f6e01-93a6-493d-85f6-4ef780a2e29e','查看详情',0,'SYSMGR.NLOG.DTL','4d379b0aefeebb49a506caaad27adcf7','1712a6bc430e16255b9b9429b83c3bfe','','nLogDetails','',1,'0','NLog','NLogDetails'),('ab7f6e01-93a6-498c-a41c-79b1ead8e99c','预览角色详情',0,'SYSMGR.ROLES.ROLEUSERS','ee65810d56f9d6badda20f9141773908','2eddc7c509391025f0b82c63b85b5096','','roleUsers','',1,'0','SysRole','RoleUsers'),('ab7f6e01-93a6-498d-8926-ff58ef93f7c4','配置用户分部',0,'SYSMGR.USER.USERATTRS','eb428e6eb500ac2cc66a88474343b5f7','ca34cbc8c380e8877b07ee5d5cc71a94','','userAttrs','',3,'0','SysUser','UserAttrs'),('ab7f6e01-93a6-4afa-b012-49220ef042d8','暂停任务',0,'SYSMGR.QUARZT.STOP','66ea295bb9c9446bbf5a8b988ae9ffeb','6341e062559ce117ccc4e0f69dc873b8','','stopQuarzt','',2,'0','Quarzt','StopQuarzt'),('ab7f6e01-93a6-4b75-935d-7406d4669d93','角色管理',1,'SYSMGR.ROLES','2eddc7c509391025f0b82c63b85b5096','54a06f6c1db9874d4b955098fed65b1a','','roleIndex','menu-icon fa fa-caret-right',2,'0','SysRole','RoleIndex'),('ab7f6e01-93a6-4d01-bd37-4f0c7902276e','预览用户详情',0,'SYSMGR.USERDETAILS','e3c86fadd93ab301a174e952aa7873b1','ca34cbc8c380e8877b07ee5d5cc71a94','','userDetails','',1,'0','SysUser','UserDetails'),('ab7f6e01-93a6-4d4e-8f47-db1bf2a43200','操作日志',1,'SYSMGR.ACTIVITYLOG','e4d16262cd1ca7e243f826204416f686','54a06f6c1db9874d4b955098fed65b1a','','activityLogIndex','menu-icon fa fa-caret-right',3,'0','ActivityLog','ActivityLogIndex'),('ab7f6e01-93a6-4dcd-b376-3bf5b2f5501c','删除角色',0,'SYSMGR.ROLES.DELETE','447f2a4ded961cc568a14e33729bdc01','2eddc7c509391025f0b82c63b85b5096','','roleDelete','',4,'0','SysRole','Delete'),('ab7f6e01-93a6-4ec7-a750-44939661d8dc','预览日志详情',0,'SYSMGR.ACTIVITYLOG.DTL','79e110058269eee535a10be0225b239d','e4d16262cd1ca7e243f826204416f686','','activityLogDetails','',1,'0','ActivityLog','ActivityLogDetails'),('ab7f6e01-93a6-4f84-8345-502d01f6ad49','删除关联用户',0,'SYSMGR.ROLES.ROLEUSERS.DEL','3ac416a7f4c281b310bb621b096c5e70','ee65810d56f9d6badda20f9141773908','','deleteRoleUsers','',1,'0','SysRole','DeleteRoleUsers'),('ab7f6e01-93a6-4f99-8656-ca1e81872ce3','启动任务',0,'SYSMGR.QUARZT.START','c44529fb014b5dc60d2c1fd2fcfd6a07','44d83b3550ae71292cd98f0288017164','','initCategory','',1,'0','Category','InitCategory'),('ab7f6e01-93a6-4fd3-a12e-12530bf566cc','任务调度',1,'SYSMGR.QUARZT','6341e062559ce117ccc4e0f69dc873b8','54a06f6c1db9874d4b955098fed65b1a','','quarztIndex','menu-icon fa fa-caret-right',7,'0','Quarzt','QuarztIndex'),('bd936e01-a074-4fe3-a2fe-0cd1e98fa3df','OSS管理',1,'OSSMGR','734f7e1425a30f2dc064c4e69384a631','','','','fa fa-folder-open-o',1,'0','',''),('bd936e01-a274-4120-a18a-1b9df201c43d','新增、修改',0,'OSSMGR.BUCKET.EDIT','0b4cf537927ce6470ccc7a42a655813c','829256f41c2bb7011e1cfa419c86773e','','editBucket','',1,'0','Bucket','EditBucket'),('bd936e01-a274-4fe4-9c5c-4224d902076d','Bucket',1,'OSSMGR.BUCKET','829256f41c2bb7011e1cfa419c86773e','734f7e1425a30f2dc064c4e69384a631','','bucketIndex','menu-icon fa fa-caret-right',1,'0','Bucket','BucketIndex'),('e4a16e01-f540-404b-ac3b-9363a5ca1e16','新增剪裁尺寸',0,'OSSMGR.BUCKET.EDITCUT','e283fe19830a2e4672852abc7f50570a','829256f41c2bb7011e1cfa419c86773e','','editBucketCut','',2,'0','BucketCut','EditBucketCut'),('eda16e01-2d9f-4331-b3f2-d8c8f1f3c4c2','删除剪裁尺寸',0,'OSSMGR.BUCKET.DELETECUT','8d4eb8daf62e867535d9dab5b18d1c79','829256f41c2bb7011e1cfa419c86773e','','deleteBucketCute','',3,'0','BucketCut','Delete');
/*!40000 ALTER TABLE `sys_category` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `sys_nlog`
--

DROP TABLE IF EXISTS `sys_nlog`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `sys_nlog` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `traceid` varchar(45) CHARACTER SET utf8 COLLATE utf8_general_ci DEFAULT NULL,
  `logged` datetime DEFAULT NULL,
  `level` varchar(45) DEFAULT NULL,
  `message` text,
  `logger` text,
  `callsite` text,
  `exception` text,
  `clientip` varchar(45) DEFAULT NULL,
  `category` varchar(450) CHARACTER SET utf8 COLLATE utf8_general_ci DEFAULT NULL,
  `eventid` varchar(45) CHARACTER SET utf8 COLLATE utf8_general_ci DEFAULT NULL,
  `properties` text,
  `user` varchar(450) CHARACTER SET utf8 COLLATE utf8_general_ci DEFAULT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=39 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci COMMENT='错误日志';
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `sys_nlog`
--

LOCK TABLES `sys_nlog` WRITE;
/*!40000 ALTER TABLE `sys_nlog` DISABLE KEYS */;
/*!40000 ALTER TABLE `sys_nlog` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `sys_permission`
--

DROP TABLE IF EXISTS `sys_permission`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `sys_permission` (
  `id` varchar(36) NOT NULL,
  `role_id` varchar(36) DEFAULT NULL,
  `category_id` varchar(36) DEFAULT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `sys_permission`
--

LOCK TABLES `sys_permission` WRITE;
/*!40000 ALTER TABLE `sys_permission` DISABLE KEYS */;
INSERT INTO `sys_permission` VALUES ('57a86e01-d632-418c-95ce-7b1b4b1d3d12','e98e6e01-aeaf-4cb7-bcb3-021d65d5d9eb','bd936e01-a074-4fe3-a2fe-0cd1e98fa3df'),('57a86e01-e732-4189-a510-02f471052ced','e98e6e01-aeaf-4cb7-bcb3-021d65d5d9eb','ab7f6e01-93a6-441c-9ff0-07087655f026'),('57a86e01-e732-4199-a97e-b12605b77a8f','e98e6e01-aeaf-4cb7-bcb3-021d65d5d9eb','ab7f6e01-91a6-43eb-9e67-0a959095e550'),('57a86e01-e732-41b3-9bdf-8249f444fabe','e98e6e01-aeaf-4cb7-bcb3-021d65d5d9eb','eda16e01-2d9f-4331-b3f2-d8c8f1f3c4c2'),('57a86e01-e732-421c-b81f-6e51a8b1de8f','e98e6e01-aeaf-4cb7-bcb3-021d65d5d9eb','ab7f6e01-93a6-4dcd-b376-3bf5b2f5501c'),('57a86e01-e732-424a-9cf7-156adb139d95','e98e6e01-aeaf-4cb7-bcb3-021d65d5d9eb','bd936e01-a274-4120-a18a-1b9df201c43d'),('57a86e01-e732-42c8-b351-bc3a51165c08','e98e6e01-aeaf-4cb7-bcb3-021d65d5d9eb','ab7f6e01-93a6-498d-8926-ff58ef93f7c4'),('57a86e01-e732-4355-89c1-7fe0fde8a487','e98e6e01-aeaf-4cb7-bcb3-021d65d5d9eb','ab7f6e01-93a6-43e8-b5e7-89f610e09547'),('57a86e01-e732-4375-8b6e-919d5280d1cd','e98e6e01-aeaf-4cb7-bcb3-021d65d5d9eb','ab7f6e01-93a6-439c-84c0-2750beccb315'),('57a86e01-e732-43f7-baf4-20a9fe266ea9','e98e6e01-aeaf-4cb7-bcb3-021d65d5d9eb','ab7f6e01-93a6-4afa-b012-49220ef042d8'),('57a86e01-e732-44e1-9894-720c9093d92e','e98e6e01-aeaf-4cb7-bcb3-021d65d5d9eb','bd936e01-a274-4fe4-9c5c-4224d902076d'),('57a86e01-e732-453d-9cb6-8f2c0621389e','e98e6e01-aeaf-4cb7-bcb3-021d65d5d9eb','ab7f6e01-93a6-4479-a90b-ce52002f8f81'),('57a86e01-e732-456b-88af-20b0ddc905d8','e98e6e01-aeaf-4cb7-bcb3-021d65d5d9eb','ab7f6e01-93a6-4f99-8656-ca1e81872ce3'),('57a86e01-e732-458e-b3ee-a512faa8bbf3','e98e6e01-aeaf-4cb7-bcb3-021d65d5d9eb','ab7f6e01-93a6-4455-8d27-d1700baa80b9'),('57a86e01-e732-45b3-9e7c-6b13a6afe115','e98e6e01-aeaf-4cb7-bcb3-021d65d5d9eb','ab7f6e01-93a6-4d4e-8f47-db1bf2a43200'),('57a86e01-e732-47b6-b1ac-48921a919ecb','e98e6e01-aeaf-4cb7-bcb3-021d65d5d9eb','ab7f6e01-93a6-4f84-8345-502d01f6ad49'),('57a86e01-e732-4837-9d66-b957c6abd90b','e98e6e01-aeaf-4cb7-bcb3-021d65d5d9eb','ab7f6e01-93a6-498c-a41c-79b1ead8e99c'),('57a86e01-e732-4931-99ab-c6d811e24a0e','e98e6e01-aeaf-4cb7-bcb3-021d65d5d9eb','0fa26e01-19ce-4520-8c4b-0096f60bf7de'),('57a86e01-e732-4960-8c4a-50ce888bb398','e98e6e01-aeaf-4cb7-bcb3-021d65d5d9eb','ab7f6e01-93a6-4b75-935d-7406d4669d93'),('57a86e01-e732-4994-b026-9660b6aa47e6','e98e6e01-aeaf-4cb7-bcb3-021d65d5d9eb','ab7f6e01-93a6-4ec7-a750-44939661d8dc'),('57a86e01-e732-4a4b-8f05-a9877c336cd0','e98e6e01-aeaf-4cb7-bcb3-021d65d5d9eb','ab7f6e01-93a6-4fd3-a12e-12530bf566cc'),('57a86e01-e732-4b2e-ac73-f3fe61ac67a4','e98e6e01-aeaf-4cb7-bcb3-021d65d5d9eb','ab7f6e01-93a6-4559-97ec-8699eb8cec30'),('57a86e01-e732-4b6d-9177-c075896d2842','e98e6e01-aeaf-4cb7-bcb3-021d65d5d9eb','ab7f6e01-93a6-493d-85f6-4ef780a2e29e'),('57a86e01-e732-4b79-bd60-04eb1d74cae2','e98e6e01-aeaf-4cb7-bcb3-021d65d5d9eb','ab7f6e01-93a6-47cf-84c8-d5517b2878b7'),('57a86e01-e732-4bb3-af22-bcc90ab637bf','e98e6e01-aeaf-4cb7-bcb3-021d65d5d9eb','ab7f6e01-93a6-485e-b439-cf823d858475'),('57a86e01-e732-4bf6-bd01-b7bf655abd15','e98e6e01-aeaf-4cb7-bcb3-021d65d5d9eb','ab7f6e01-93a6-4333-9211-65333bed7de7'),('57a86e01-e732-4ccf-9935-a02ac26df77a','e98e6e01-aeaf-4cb7-bcb3-021d65d5d9eb','e4a16e01-f540-404b-ac3b-9363a5ca1e16'),('57a86e01-e732-4e0a-98c2-b77f8c989e28','e98e6e01-aeaf-4cb7-bcb3-021d65d5d9eb','ab7f6e01-93a6-4d01-bd37-4f0c7902276e'),('57a86e01-e732-4eb1-9f15-e0773a3dc600','e98e6e01-aeaf-4cb7-bcb3-021d65d5d9eb','ab7f6e01-93a6-4157-a4fd-b53bbd6b435c'),('57a86e01-e732-4ec0-b6c7-bdce39d11a1b','e98e6e01-aeaf-4cb7-bcb3-021d65d5d9eb','ab7f6e01-93a6-4181-8b36-4a16279fe91f'),('57a86e01-e732-4f23-bca8-8b1272234d9d','e98e6e01-aeaf-4cb7-bcb3-021d65d5d9eb','058a6e01-4238-4b4a-9dac-31293dd60502'),('57a86e01-e732-4f2d-9903-ee9a907044d1','e98e6e01-aeaf-4cb7-bcb3-021d65d5d9eb','ab7f6e01-93a6-48f2-9c46-dfd272bee0b8');
/*!40000 ALTER TABLE `sys_permission` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `sys_role`
--

DROP TABLE IF EXISTS `sys_role`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `sys_role` (
  `id` varchar(36) NOT NULL,
  `creator` varchar(36) DEFAULT NULL,
  `creation_time` datetime NOT NULL,
  `description` varchar(450) CHARACTER SET utf8 COLLATE utf8_general_ci DEFAULT NULL,
  `name` varchar(45) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `sys_role`
--

LOCK TABLES `sys_role` WRITE;
/*!40000 ALTER TABLE `sys_role` DISABLE KEYS */;
INSERT INTO `sys_role` VALUES ('e98e6e01-aeaf-4cb7-bcb3-021d65d5d9eb','e12d5344-0a9f-11ea-93cb-000c29f1800f','2019-11-21 17:01:50',NULL,'管理员');
/*!40000 ALTER TABLE `sys_role` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `sys_setting`
--

DROP TABLE IF EXISTS `sys_setting`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `sys_setting` (
  `id` varchar(36) NOT NULL,
  `name` varchar(64) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL,
  `value` varchar(450) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `sys_setting`
--

LOCK TABLES `sys_setting` WRITE;
/*!40000 ALTER TABLE `sys_setting` DISABLE KEYS */;
INSERT INTO `sys_setting` VALUES ('11a36e01-477c-487c-b3b3-d7fa09ec87a6','OSSAccessKeyId','oss-key-123'),('11a36e01-567c-4e86-8f50-ee18f084d5fc','OSSAccessKeySecret','51886b9c29f81dc89ed3de4c652909fd'),('ab7f6e01-b1b9-4ca5-bdf7-112666a1162f','CompanyName',''),('ab7f6e01-b5b9-409d-b4b3-defd896fbf30','EmailErrorPush','0'),('ab7f6e01-b5b9-4224-a551-1974c1c7bc85','EmailAccount',''),('ab7f6e01-b5b9-45a8-8192-7bfaacf2b6d6','EmailHost','smtp.qq.com'),('ab7f6e01-b5b9-460b-837e-2d18d1cba965','EmailPassword','587'),('ab7f6e01-b5b9-46df-9363-a77d0d3f6f3c','ErrorToMailAddress',''),('ab7f6e01-b5b9-4772-89cb-93df70d5ed37','SiteName','Fast管理中心'),('ab7f6e01-b5b9-4820-b887-9f8612a82c7c','JPushApk',''),('ab7f6e01-b5b9-4f08-a414-e65b206fd033','EmailPort',''),('ab7f6e01-b5b9-4f49-b90b-001257c5e158','JPushSecret','');
/*!40000 ALTER TABLE `sys_setting` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `sys_user`
--

DROP TABLE IF EXISTS `sys_user`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `sys_user` (
  `id` varchar(36) NOT NULL,
  `account` varchar(45) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL,
  `name` varchar(45) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL,
  `password` varchar(512) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL,
  `salt` varchar(256) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL,
  `is_admin` tinyint(1) NOT NULL DEFAULT '0',
  `creation_time` datetime NOT NULL,
  `is_deleted` tinyint(1) NOT NULL DEFAULT '0',
  `deleted_time` datetime DEFAULT NULL,
  `creator` varchar(36) DEFAULT NULL,
  `last_ipaddr` varchar(45) DEFAULT NULL,
  `last_activity_time` datetime DEFAULT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `sys_user`
--

LOCK TABLES `sys_user` WRITE;
/*!40000 ALTER TABLE `sys_user` DISABLE KEYS */;
INSERT INTO `sys_user` VALUES ('48a86e01-91bd-4ede-9139-09b13e5533d2','test','测试人员','9231c2f89bf064adffbcddd104a8edc4','x0/jhUvNMGLtr6wn0ERvDWK9LoTRYBXRa0Q3WTcJ0JQ=',0,'2019-11-26 15:16:10',0,NULL,'e12d5344-0a9f-11ea-93cb-000c29f1800f',NULL,NULL),('e12d5344-0a9f-11ea-93cb-000c29f1800f','admin','super man','6cb4f49799bf970e2609e0394338c9c6','W3b5VElyh3qX3U07GYaDH/ZpkAqzKNvjBNGoZdgtPFc=',1,'2019-11-19 15:40:38',0,NULL,NULL,'127.0.0.1',NULL);
/*!40000 ALTER TABLE `sys_user` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `sys_user_jwt`
--

DROP TABLE IF EXISTS `sys_user_jwt`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `sys_user_jwt` (
  `id` varchar(36) NOT NULL,
  `user_id` varchar(36) NOT NULL,
  `expiration` datetime NOT NULL,
  `platform` int(11) NOT NULL DEFAULT '0',
  PRIMARY KEY (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `sys_user_jwt`
--

LOCK TABLES `sys_user_jwt` WRITE;
/*!40000 ALTER TABLE `sys_user_jwt` DISABLE KEYS */;
INSERT INTO `sys_user_jwt` VALUES ('77846e01-f778-4f08-ae65-9207c115ff6c','e12d5344-0a9f-11ea-93cb-000c29f1800f','2019-12-19 16:20:53',0),('78a76e01-92ff-40af-ab74-bc58c10767de','e12d5344-0a9f-11ea-93cb-000c29f1800f','2019-12-26 11:29:15',0),('898d6e01-e6eb-4316-8aa5-587acc33b08e','e12d5344-0a9f-11ea-93cb-000c29f1800f','2019-12-21 10:37:37',0),('dea16e01-74d6-438c-aaa3-b419d891e2bd','e12d5344-0a9f-11ea-93cb-000c29f1800f','2019-12-25 09:22:46',0),('eb896e01-1dcd-47ba-a305-15ed84157584','e12d5344-0a9f-11ea-93cb-000c29f1800f','2019-12-20 17:46:02',0);
/*!40000 ALTER TABLE `sys_user_jwt` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `sys_user_login`
--

DROP TABLE IF EXISTS `sys_user_login`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `sys_user_login` (
  `id` varchar(36) NOT NULL,
  `user_id` varchar(36) DEFAULT NULL,
  `ip_addr` varchar(128) CHARACTER SET utf8 COLLATE utf8_general_ci DEFAULT NULL,
  `logged_time` datetime NOT NULL,
  `comment` varchar(145) CHARACTER SET utf8 COLLATE utf8_general_ci DEFAULT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci COMMENT='		';
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `sys_user_login`
--

LOCK TABLES `sys_user_login` WRITE;
/*!40000 ALTER TABLE `sys_user_login` DISABLE KEYS */;
INSERT INTO `sys_user_login` VALUES ('77846e01-f378-452b-8377-6e2524f51c71','e12d5344-0a9f-11ea-93cb-000c29f1800f','127.0.0.1','2019-11-19 16:20:53',NULL),('78a76e01-88ff-42bd-b177-396e0a055c7b','e12d5344-0a9f-11ea-93cb-000c29f1800f','127.0.0.1','2019-11-26 11:29:15',NULL),('898d6e01-e3eb-40e9-8f9b-42ca6a77a3d0','e12d5344-0a9f-11ea-93cb-000c29f1800f','127.0.0.1','2019-11-21 10:37:37',NULL),('dea16e01-71d6-4492-bf84-ae27e9538d62','e12d5344-0a9f-11ea-93cb-000c29f1800f','127.0.0.1','2019-11-25 09:22:46',NULL),('eb896e01-16cd-49c2-9257-a681439cf87a','e12d5344-0a9f-11ea-93cb-000c29f1800f','127.0.0.1','2019-11-20 17:46:02',NULL);
/*!40000 ALTER TABLE `sys_user_login` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `sys_user_role`
--

DROP TABLE IF EXISTS `sys_user_role`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `sys_user_role` (
  `id` varchar(36) NOT NULL,
  `user_id` varchar(36) NOT NULL,
  `role_id` varchar(36) NOT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `sys_user_role`
--

LOCK TABLES `sys_user_role` WRITE;
/*!40000 ALTER TABLE `sys_user_role` DISABLE KEYS */;
INSERT INTO `sys_user_role` VALUES ('4ca86e01-0bde-4fa5-bc09-b07a671ec6b4','48a86e01-91bd-4ede-9139-09b13e5533d2','e98e6e01-aeaf-4cb7-bcb3-021d65d5d9eb');
/*!40000 ALTER TABLE `sys_user_role` ENABLE KEYS */;
UNLOCK TABLES;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2019-11-26 15:35:55
