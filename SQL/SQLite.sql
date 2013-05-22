-- 系统组织机构表，记录系统中的所有机构名称。
CREATE TABLE ORG_ORGANIZATION
(
	-- 组织机构id，使用自增列作为主键。
	ID INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,
	NAME TEXT NOT NULL,
	-- 当前组织机构的父机构id，为NULL时标识该机构为顶级机构。
	PARENTID INTEGER,
	CREATETIME TEXT DEFAULT (DATETIME(CURRENT_TIMESTAMP,'localtime')) NOT NULL,
	-- 标识该机构是否已经删除，系统中的机构不允许进行物理删除。
	ISDELETE TEXT DEFAULT 'N' NOT NULL,
	-- 排序序号，控制显示顺序。
	[ORDER] INTEGER,
	FOREIGN KEY (PARENTID) REFERENCES ORG_ORGANIZATION (ID)
);

--用户表
CREATE TABLE ORG_USER
(
	-- 用户id，使用自增列作为主键。
	ID INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,
	-- 用户登陆名称，使用唯一索引不允许重复。
	LOGNAME TEXT NOT NULL UNIQUE,
	-- 用户登陆密码，使用MD5加密算法加密存储。
	LOGPASSWORD TEXT NOT NULL,
	-- 用户姓名
	NAME TEXT NOT NULL,
	-- 用户所在组织机构的id，为空表示用户为顶级用户，不属于任何组织机构。
	ORGANIZATIONID INTEGER,
	-- 用户是否被删除。和组织机构一样，用户不允许物理删除。
	ISDELETE TEXT DEFAULT 'N',
	-- 排序序号，用来控制显示顺序。
	[ORDER] INTEGER,
	FOREIGN KEY (ORGANIZATIONID) REFERENCES ORG_ORGANIZATION (ID)
);

--插入用户表基本数据。
INSERT INTO org_user (logName, logPassword, name) VALUES ('root', 'b9be11166d72e9e3ae7fd407165e4bd2', '超级用户');
INSERT INTO org_user (logName, logPassword, name) VALUES ('admin', 'c3284d0f94606de1fd2af172aba15bf3', '超级管理员');

--系统菜单表
CREATE TABLE SYS_MENUS
(
	-- 菜单id，使用自增列做主键。
	ID INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,
	-- 菜单名称，记录菜单显示的名称。
	NAME TEXT NOT NULL,
	-- 菜单地址，根据实际情况设置相对路径和绝对路径。
	URL TEXT,
	-- 父菜单ID，为NULL表示顶级菜单。
	PARENTID INTEGER,
	-- 菜单前面显示的图标，使用JQuery EasyUI中的图标字符串类型。
	ICON TEXT,
	-- 桌面图标，记录图片相对路径。
	DESKTOPICON TEXT,
	-- 用来控制菜单的显示顺序。
	[ORDER] INTEGER DEFAULT 0 NOT NULL,
	FOREIGN KEY (PARENTID) REFERENCES SYS_MENUS (ID)
);

--插入系统菜单表基础数据。
INSERT INTO sys_menus (name,icon) VALUES ('系统设置','icon-systemSetting');
INSERT INTO sys_menus (name,icon) VALUES ('系统管理','icon-system');
INSERT INTO sys_menus (name, url, parentID, icon) VALUES ('系统菜单', 'sys/menu/menu_list.aspx', '1', 'icon-systemMenu');
INSERT INTO sys_menus (name, url, parentID, icon) VALUES ('角色管理', 'sys/role/role_list.aspx', '2','icon-role');
INSERT INTO sys_menus (name, url, parentID, icon) VALUES ('组织机构管理', 'sys/organization/organization_list.aspx', '2', 'icon-organization');
INSERT INTO sys_menus (name, url, parentID, icon) VALUES ('数据字典', 'sys/dataDictionary/dataDictionary_list.aspx', '1', 'icon-dictionary');

--系统菜单页面关联表
CREATE TABLE SYS_MENU_PAGE
(
	ID INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,
	-- 页面说明
	DETAIL TEXT,
	-- 要关联的菜单相对路径。
	PATH TEXT NOT NULL,
	-- 菜单id，使用自增列做主键。
	MENUID INTEGER NOT NULL,
	FOREIGN KEY (MENUID) REFERENCES SYS_MENUS (ID)
);

--创建角色菜单关联表。
CREATE TABLE AUT_ROLE
(
	ID INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,
	NAME TEXT NOT NULL,
	EXPLAIN TEXT
);

--创建角色菜单关联表
CREATE TABLE AUT_ROLE_MENU
(
	ROLEID INTEGER NOT NULL,
	-- 菜单id，使用自增列做主键。
	MENUID INTEGER NOT NULL,
	PRIMARY KEY (ROLEID, MENUID),
	FOREIGN KEY (ROLEID) REFERENCES AUT_ROLE (ID),
	FOREIGN KEY (MENUID) REFERENCES SYS_MENUS (ID)
);

--创建用户角色关联表。
CREATE TABLE AUT_USER_ROLE
(
	-- 用户id，使用自增列作为主键。
	USERID INTEGER NOT NULL,
	ROLEID INTEGER NOT NULL,
	PRIMARY KEY (USERID, ROLEID),
	FOREIGN KEY (USERID) REFERENCES ORG_USER (ID),
	FOREIGN KEY (ROLEID) REFERENCES AUT_ROLE (ID)
);

--系统数据字典表
CREATE TABLE SYS_DATADICTIONARY
(
	ID INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,
	-- 为NULL表示顶级字典。
	PARENTID INTEGER,
	NAME TEXT NOT NULL,
	VALUE INTEGER NOT NULL,
	CODE TEXT NOT NULL,
	[ORDER] INTEGER,
	FOREIGN KEY (PARENTID) REFERENCES SYS_DATADICTIONARY (ID)
);