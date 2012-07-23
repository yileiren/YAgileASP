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
	PRIMARY KEY (ID)
);
GO

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
	PRIMARY KEY (ID)
);
GO


/* Create Foreign Keys */

ALTER TABLE ORG_USER
	ADD FOREIGN KEY (ORGANIZATIONID)
	REFERENCES ORG_ORGANIZATION (ID)
;
GO

--�����û���������ݡ�
INSERT INTO org_user (logName, logPassword, name) VALUES ('root', '63a9f0ea7bb98050796b649e85481845', '�����û�');
INSERT INTO org_user (logName, logPassword, name) VALUES ('admin', '21232f297a57a5a743894a0e4a801fc3', '��������Ա');
GO