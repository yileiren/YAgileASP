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
	PRIMARY KEY (ID)
);
GO

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
	PRIMARY KEY (ID)
);
GO


/* Create Foreign Keys */

ALTER TABLE ORG_USER
	ADD FOREIGN KEY (ORGANIZATIONID)
	REFERENCES ORG_ORGANIZATION (ID)
;
GO

--插入用户表基本数据。
INSERT INTO org_user (logName, logPassword, name) VALUES ('root', '63a9f0ea7bb98050796b649e85481845', '超级用户');
INSERT INTO org_user (logName, logPassword, name) VALUES ('admin', '21232f297a57a5a743894a0e4a801fc3', '超级管理员');
GO