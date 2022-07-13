CREATE SCHEMA IF NOT EXISTS sysmanager;
USE sysmanager;

-- ---------------------------
-- tabela de usuários
-- ---------------------------
CREATE TABLE IF NOT EXISTS sysmanager.user
(
`id` char(36) NOT NULL DEFAULT 'uuid()' COMMENT 'Identificador unico do registro',
`userName` varchar(50) NOT NULL COMMENT 'nome do usuário',
`email` varchar(100) NOT NULL COMMENT 'email do usuário',
`password` varchar(50) NOT NULL COMMENT 'senha do usuário',
`active` bit NOT NULL DEFAULT false COMMENT 'indicador se o usuário esta ativo ou inativo',
PRIMARY KEY(`id`)
);

-- ---------------------------
-- tabela de produtos
-- ---------------------------
CREATE TABLE IF NOT EXISTS sysManager.product
(
`id` char(36) NOT NULL DEFAULT 'uuid()' COMMENT 'Identificador unico do registro',
`productCode` varchar(50) NOT NULL COMMENT 'Codigo do produto',
`name` varchar(50) NOT NULL COMMENT 'nome/Descrição do produto',
`productTypeId`  varchar(50) NOT NULL COMMENT 'tipo do produto',
`categoryId`  varchar(50) NOT NULL COMMENT 'categoria do produto',
`unityId`  varchar(50) NOT NULL COMMENT 'unidade de medida do produto',
`costPrice` decimal DEFAULT 0  COMMENT 'preço de custo do produto',
`percentage` decimal DEFAULT 0  COMMENT 'percentual de venda do produto',
`price` decimal DEFAULT 0  COMMENT 'preço final do produto',
`active` bit NOT NULL DEFAULT false COMMENT 'indicador se o usuário esta ativo ou inativo',
`creationDate` DateTime NOT NULL DEFAULT NOW() COMMENT 'data de criação do registro',
`updateDate` DateTime NULL COMMENT 'data de atualização do registro',
PRIMARY KEY(`id`),
CONSTRAINT `fk_producType` FOREIGN KEY (`productTypeId`) REFERENCES sysManager.productType(`id`),
CONSTRAINT `fk_category` FOREIGN KEY (`categoryId`) REFERENCES sysManager.category(`id`),
CONSTRAINT `fk_unity` FOREIGN KEY (`unityId`) REFERENCES sysManager.unity(`id`)
ON DELETE NO ACTION
ON UPDATE NO ACTION
);


-- -----------------------------------------------------
-- Table `sysManager`.`productType`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS sysmanager.productType (
  `id` CHAR(36) not null default 'uuid()' comment 'Identificador do registro',
  `name` varchar(100) not null comment 'Nome do Tipo de produto',
  `active` bit NOT NULL default false comment 'Ativo ou inativo',
  `createdDate` datetime not null default NOW() comment 'data de criação do registro',
  `updatedDate` datetime null comment 'data de atualização do registro',
  PRIMARY KEY (`id`)
  );

-- -----------------------------------------------------
-- Table `sysManager`.`unity`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS sysManager.unity (
  `id` CHAR(36) not null default 'uuid()' comment 'Identificador do registro',
  `name` varchar(100) not null comment 'Nome do Unidade de produto',
  `active` bit NOT NULL default false comment 'Ativo ou inativo',
  `createdDate` datetime not null default NOW() comment 'data de criação do registro',
  `updatedDate` datetime null  comment 'data de atualização do registro',
  PRIMARY KEY (`id`)
  );

-- -----------------------------------------------------
-- Table `sysManager`.`category`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS sysManager.category (
  `id` CHAR(36) not null default 'uuid()' comment 'Identificador do registro',
  `name` varchar(100) not null comment 'Nome do categoria de produto',
  `active` bit NOT NULL default false comment 'Ativo ou inativo',
  `createdDate` datetime not null default NOW() comment 'data de criação do registro',
  `updatedDate` datetime null comment 'data de atualização do registro',
  PRIMARY KEY (`id`)
  );