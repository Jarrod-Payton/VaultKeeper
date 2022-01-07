CREATE TABLE IF NOT EXISTS accounts(
  id VARCHAR(255) NOT NULL primary key COMMENT 'primary key',
  createdAt DATETIME DEFAULT CURRENT_TIMESTAMP COMMENT 'Time Created',
  updatedAt DATETIME DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP COMMENT 'Last Update',
  name varchar(255) COMMENT 'User Name',
  email varchar(255) COMMENT 'User Email',
  picture varchar(255) COMMENT 'User Picture'
) default charset utf8 COMMENT '';
CREATE TABLE IF NOT EXISTS keeps(
  id INT AUTO_INCREMENT NOT NULL PRIMARY KEY,
  creatorId VARCHAR(255) NOT NULL,
  name TEXT NOT NULL,
  description TEXT NOT NULL,
  img TEXT NOT NULL,
  views INT NOT NULL,
  keeps INT NOT NULL,
  shares INT NOT NULL,
  FOREIGN KEY (creatorId) REFERENCES accounts(id) ON DELETE CASCADE
) default charset utf8 COMMENT '';
CREATE TABLE IF NOT EXISTS vaults(
  id INT AUTO_INCREMENT NOT NULL PRIMARY KEY,
  creatorId VARCHAR(255) NOT NULL,
  name TEXT NOT NULL,
  description TEXT NOT NULL,
  isPrivate TINYINT NOT NULL,
  FOREIGN KEY (creatorId) REFERENCES accounts(id) ON DELETE CASCADE
) default charset utf8 COMMENT '';
CREATE TABLE IF NOT EXISTS vaultKeeps(
  id INT AUTO_INCREMENT NOT NULL PRIMARY KEY,
  creatorId VARCHAR(255) NOT NULL,
  vaultId INT NOT NULL,
  keepId INT NOT NULL,
  FOREIGN KEY (creatorId) REFERENCES accounts(id) ON DELETE CASCADE,
  FOREIGN KEY (vaultId) REFERENCES vaults(id) ON DELETE CASCADE,
  FOREIGN KEY (keepId) REFERENCES keeps(id) ON DELETE CASCADE
) default charset utf8 COMMENT '';
INSERT INTO
  keeps (
    creatorId,
    name,
    description,
    img,
    views,
    keeps,
    shares
  )
VALUES
  (
    "61d77f848580c7d11aa6c4e4",
    "Hello",
    "this is a cool album",
    "hello",
    0,
    0,
    0
  );
SELECT
  *
FROM
  keeps
WHERE
  creatorId = "61d77f848580c7d11aa6c4e4";
SELECT
  a.*,
  a.id AS accountId,
  k.*
FROM
  keeps k
  JOIN accounts a ON k.creatorId = a.id
WHERE
  k.id = 1;