/* Create Tables */

-- 系统组织机构表，记录系统中的所有机构名称。
CREATE TABLE ORG_ORGANIZATION
(
	-- 组织机构id，使用自增列作为主键。
	ID INT NOT NULL IDENTITY (1, 1),
	NAME NVARCHAR(50) NOT NULL,
	-- 当前组织机构的父机构id，为NULL时标识该机构为顶级机构。
	PARENTID INT DEFAULT NULL,
	CREATETIME DATETIME DEFAULT GETDATE() NOT NULL,
	-- 标识该机构是否已经删除，系统中的机构不允许进行物理删除。
	ISDELETE NCHAR DEFAULT 'N' NOT NULL,
	-- 排序序号，控制显示顺序。
	[ORDER] INT,
	PRIMARY KEY (ID)
);
GO

--创建用户表
CREATE TABLE ORG_USER
(
	-- 用户id，使用自增列作为主键。
	ID INT NOT NULL IDENTITY ,
	-- 用户登陆名称，使用唯一索引不允许重复。
	LOGNAME NVARCHAR(20) NOT NULL UNIQUE,
	-- 用户登陆密码，使用MD5加密算法加密存储。
	LOGPASSWORD NVARCHAR(40) NOT NULL,
	-- 用户姓名
	NAME NVARCHAR(20) NOT NULL,
	-- 用户所在组织机构的id，为空表示用户为顶级用户，不属于任何组织机构。
	ORGANIZATIONID INT,
	-- 用户是否被删除。和组织机构一样，用户不允许物理删除。
	ISDELETE NCHAR DEFAULT 'N',
	-- 排序序号，用来控制显示顺序。
	[ORDER] INT,
	PRIMARY KEY (ID)
);
GO


--用户表创建外键
ALTER TABLE ORG_USER
	ADD FOREIGN KEY (ORGANIZATIONID)
	REFERENCES ORG_ORGANIZATION (ID)
;
GO

--插入用户表基本数据。
INSERT INTO org_user (logName, logPassword, name) VALUES ('root', 'b9be11166d72e9e3ae7fd407165e4bd2', '超级用户');
INSERT INTO org_user (logName, logPassword, name) VALUES ('admin', 'c3284d0f94606de1fd2af172aba15bf3', '超级管理员');
GO


--创建系统菜单表。
CREATE TABLE SYS_MENUS
(
	-- 菜单id，使用自增列做主键。
	ID INT NOT NULL IDENTITY ,
	-- 菜单名称，记录菜单显示的名称。
	NAME NVARCHAR(20) NOT NULL,
	-- 菜单地址，根据实际情况设置相对路径和绝对路径。
	URL NVARCHAR(200),
	-- 父菜单ID，为NULL表示顶级菜单。
	PARENTID INT,
	-- 菜单前面显示的图标，使用JQuery EasyUI中的图标字符串类型。
	ICON NVARCHAR(20),
	-- 桌面图标，记录图片相对路径。
	DESKTOPICON NVARCHAR(100),
	-- 用来控制菜单的显示顺序。
	[ORDER] INT DEFAULT 0 NOT NULL,
	PRIMARY KEY (ID)
);
GO

--插入系统菜单表基础数据。
INSERT INTO sys_menus (name,icon) VALUES ('系统设置','icon-systemSetting')
INSERT INTO sys_menus (name,icon) VALUES ('系统管理','icon-system');
INSERT INTO sys_menus (name, url, parentID, icon) VALUES ('系统菜单', 'sys/menu/menu_list.aspx', '1', 'icon-systemMenu');
INSERT INTO sys_menus (name, url, parentID, icon) VALUES ('角色管理', 'sys/role/role_list.aspx', '2','icon-role');
INSERT INTO sys_menus (name, url, parentID, icon) VALUES ('组织机构管理', 'sys/organization/organization_list.aspx', '2', 'icon-organization');
GO

--创建角色表。
CREATE TABLE AUT_ROLE
(
	ID INT NOT NULL IDENTITY ,
	NAME NVARCHAR(20) NOT NULL,
	EXPLAIN NVARCHAR(50),
	PRIMARY KEY (ID)
);
GO

--创建角色菜单关联表。
CREATE TABLE AUT_ROLE_MENU
(
	ROLEID INT NOT NULL,
	-- 菜单id，使用自增列做主键。
	MENUID INT NOT NULL,
	PRIMARY KEY (ROLEID, MENUID)
);
GO

--创建角色菜单关联表中的角色id外键
ALTER TABLE AUT_ROLE_MENU
	ADD FOREIGN KEY (ROLEID)
	REFERENCES AUT_ROLE (ID);
GO

--创建角色菜单关联表中的菜单id外键
ALTER TABLE AUT_ROLE_MENU
	ADD FOREIGN KEY (MENUID)
	REFERENCES SYS_MENUS (ID);
GO