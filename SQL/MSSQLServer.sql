/* Create Tables */

-- ϵͳ��֯��������¼ϵͳ�е����л������ơ�
CREATE TABLE ORG_ORGANIZATION
(
	-- ��֯����id��ʹ����������Ϊ������
	ID INT NOT NULL IDENTITY (1, 1),
	NAME NVARCHAR(50) NOT NULL,
	-- ��ǰ��֯�����ĸ�����id��ΪNULLʱ��ʶ�û���Ϊ����������
	PARENTID INT DEFAULT NULL,
	CREATETIME DATETIME DEFAULT GETDATE() NOT NULL,
	-- ��ʶ�û����Ƿ��Ѿ�ɾ����ϵͳ�еĻ����������������ɾ����
	ISDELETE NCHAR DEFAULT 'N' NOT NULL,
	-- ������ţ�������ʾ˳��
	[ORDER] INT,
	PRIMARY KEY (ID),
	--�����ӣ���Ǹ�id
	FOREIGN KEY (PARENTID) REFERENCES ORG_ORGANIZATION (ID)
);
GO

--�����û���
CREATE TABLE ORG_USER
(
	-- �û�id��ʹ����������Ϊ������
	ID INT NOT NULL IDENTITY ,
	-- �û���½���ƣ�ʹ��Ψһ�����������ظ���
	LOGNAME NVARCHAR(20) NOT NULL UNIQUE,
	-- �û���½���룬ʹ��MD5�����㷨���ܴ洢��
	LOGPASSWORD NVARCHAR(40) NOT NULL,
	-- �û�����
	NAME NVARCHAR(20) NOT NULL,
	-- �û�������֯������id��Ϊ�ձ�ʾ�û�Ϊ�����û����������κ���֯������
	ORGANIZATIONID INT,
	-- �û��Ƿ�ɾ��������֯����һ�����û�����������ɾ����
	ISDELETE NCHAR DEFAULT 'N',
	-- ������ţ�����������ʾ˳��
	[ORDER] INT,
	PRIMARY KEY (ID),
	--�û��������������֯����id
	FOREIGN KEY (ORGANIZATIONID) REFERENCES ORG_ORGANIZATION (ID)
);
GO

--�����û���������ݡ�
INSERT INTO org_user (logName, logPassword, name) VALUES ('root', 'b9be11166d72e9e3ae7fd407165e4bd2', '�����û�');
INSERT INTO org_user (logName, logPassword, name) VALUES ('admin', 'c3284d0f94606de1fd2af172aba15bf3', '��������Ա');
GO


--����ϵͳ�˵���
CREATE TABLE SYS_MENUS
(
	-- �˵�id��ʹ����������������
	ID INT NOT NULL IDENTITY ,
	-- �˵����ƣ���¼�˵���ʾ�����ơ�
	NAME NVARCHAR(20) NOT NULL,
	-- �˵���ַ������ʵ������������·���;���·����
	URL NVARCHAR(200),
	-- ���˵�ID��ΪNULL��ʾ�����˵���
	PARENTID INT,
	-- �˵�ǰ����ʾ��ͼ�꣬ʹ��JQuery EasyUI�е�ͼ���ַ������͡�
	ICON NVARCHAR(20),
	-- ����ͼ�꣬��¼ͼƬ���·����
	DESKTOPICON NVARCHAR(100),
	-- �������Ʋ˵�����ʾ˳��
	[ORDER] INT DEFAULT 0 NOT NULL,
	PRIMARY KEY (ID),
	--�����ӣ���Ǹ��˵���
	FOREIGN KEY (PARENTID) REFERENCES SYS_MENUS (ID)
);
GO

--����ϵͳ�˵���������ݡ�
INSERT INTO sys_menus (name,icon) VALUES ('ϵͳ����','icon-systemSetting')
INSERT INTO sys_menus (name,icon) VALUES ('ϵͳ����','icon-system');
INSERT INTO sys_menus (name, url, parentID, icon) VALUES ('ϵͳ�˵�', 'sys/menu/menu_list.aspx', '1', 'icon-systemMenu');
INSERT INTO sys_menus (name, url, parentID, icon) VALUES ('��ɫ����', 'sys/role/role_list.aspx', '2','icon-role');
INSERT INTO sys_menus (name, url, parentID, icon) VALUES ('��֯��������', 'sys/organization/organization_list.aspx', '2', 'icon-organization');
INSERT INTO sys_menus (name, url, parentID, icon) VALUES ('�����ֵ�', 'sys/dataDictionary/dataDictionary_list.aspx', '1', 'icon-dictionary');
GO

--�����˵�ҳ�������
CREATE TABLE SYS_MENU_PAGE
(
	ID INT NOT NULL IDENTITY ,
	-- ҳ��˵��
	DETAIL NVARCHAR(200),
	-- Ҫ�����Ĳ˵����·����
	PATH NVARCHAR(500) NOT NULL,
	-- �˵�id��ʹ����������������
	MENUID INT NOT NULL,
	PRIMARY KEY (ID),
	--�����˵�id
	FOREIGN KEY (MENUID) REFERENCES SYS_MENUS (ID)
);

--������ɫ��
CREATE TABLE AUT_ROLE
(
	ID INT NOT NULL IDENTITY ,
	--��ɫ����
	NAME NVARCHAR(20) NOT NULL,
	--˵��
	EXPLAIN NVARCHAR(50),
	PRIMARY KEY (ID)
);
GO

--������ɫ�˵�������
CREATE TABLE AUT_ROLE_MENU
(
	ROLEID INT NOT NULL,
	-- �˵�id��ʹ����������������
	MENUID INT NOT NULL,
	PRIMARY KEY (ROLEID, MENUID),
	--������ɫ�������������ɫid
	FOREIGN KEY (ROLEID) REFERENCES AUT_ROLE (ID),
	--�����˵�������������˵�id
	FOREIGN KEY (MENUID) REFERENCES SYS_MENUS (ID)
);
GO

--�����û���ɫ������
CREATE TABLE AUT_USER_ROLE
(
	-- �û�id��ʹ����������Ϊ������
	USERID INT NOT NULL,
	ROLEID INT NOT NULL,
	PRIMARY KEY (USERID, ROLEID),
	--�����û�������������û�id
	FOREIGN KEY (USERID) REFERENCES ORG_USER (ID),
	--������ɫ�������������ɫid
	FOREIGN KEY (ROLEID) REFERENCES AUT_ROLE (ID)
);
GO

--ϵͳ�����ֵ��
CREATE TABLE SYS_DATADICTIONARY
(
	--�ֵ���id
	ID INT NOT NULL IDENTITY ,
	--��id
	PARENTID INT,
	--�ֵ�������
	NAME NVARCHAR(50) NOT NULL,
	--�ֵ����Ӧ����ֵ
	VALUE INT NOT NULL,
	--�ֵ����Ӧ�Ĵ���
	CODE NVARCHAR(50) NOT NULL,
	--�ֵ����������
	[ORDER] INT,
	PRIMARY KEY (ID),
	--�����ӣ���Ǹ��ֵ���
	FOREIGN KEY (PARENTID) REFERENCES SYS_DATADICTIONARY (ID)
);
GO