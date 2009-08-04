/*
SQLyog Enterprise - MySQL GUI v7.12 
MySQL - 5.1.36-community : Database - prosperchat
*********************************************************************
*/

/*!40101 SET NAMES utf8 */;

/*!40101 SET SQL_MODE=''*/;

/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;

CREATE DATABASE /*!32312 IF NOT EXISTS*/`prosperchat` /*!40100 DEFAULT CHARACTER SET latin1 */;

USE `prosperchat`;

/*Table structure for table `cad_mensagem` */

DROP TABLE IF EXISTS `cad_mensagem`;

CREATE TABLE `cad_mensagem` (
  `idt_mensagem` int(8) NOT NULL AUTO_INCREMENT,
  `dsc_mensagem` varchar(200) NOT NULL,
  `idt_sala` int(8) NOT NULL,
  `idt_usuario` int(8) NOT NULL,
  `dta_envio` datetime DEFAULT NULL,
  `flg_anuncio` tinyint(1) DEFAULT NULL,
  PRIMARY KEY (`idt_mensagem`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

/*Data for the table `cad_mensagem` */

/*Table structure for table `cad_mensagem_monitorada` */

DROP TABLE IF EXISTS `cad_mensagem_monitorada`;

CREATE TABLE `cad_mensagem_monitorada` (
  `idt_monitoramento` int(8) NOT NULL AUTO_INCREMENT,
  `idt_usuario` int(8) NOT NULL,
  `dsc_mensagem` varchar(200) NOT NULL,
  `dta_envio` datetime DEFAULT NULL,
  PRIMARY KEY (`idt_monitoramento`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

/*Data for the table `cad_mensagem_monitorada` */

/*Table structure for table `cad_mensagem_privada` */

DROP TABLE IF EXISTS `cad_mensagem_privada`;

CREATE TABLE `cad_mensagem_privada` (
  `idt_mensagem_privada` int(8) NOT NULL AUTO_INCREMENT,
  `dsc_mensagem` varchar(200) NOT NULL,
  `idt_remetente` int(8) NOT NULL,
  `idt_destinatario` int(8) NOT NULL,
  `dta_envio` datetime NOT NULL,
  `flg_entregue` tinyint(1) DEFAULT NULL,
  PRIMARY KEY (`idt_mensagem_privada`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

/*Data for the table `cad_mensagem_privada` */

/*Table structure for table `cad_palavra_monitorada` */

DROP TABLE IF EXISTS `cad_palavra_monitorada`;

CREATE TABLE `cad_palavra_monitorada` (
  `idt_palavra` int(8) NOT NULL AUTO_INCREMENT,
  `dsc_palavra` varchar(50) NOT NULL,
  PRIMARY KEY (`idt_palavra`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

/*Data for the table `cad_palavra_monitorada` */

/*Table structure for table `cad_sala` */

DROP TABLE IF EXISTS `cad_sala`;

CREATE TABLE `cad_sala` (
  `idt_sala` int(8) NOT NULL AUTO_INCREMENT,
  `nom_sala` varchar(30) NOT NULL,
  `flg_ativo` tinyint(1) NOT NULL,
  `cod_sala` varchar(10) NOT NULL,
  `dsc_bemvindo` varchar(500) DEFAULT NULL,
  PRIMARY KEY (`idt_sala`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

/*Data for the table `cad_sala` */

/*Table structure for table `cad_usuario` */

DROP TABLE IF EXISTS `cad_usuario`;

CREATE TABLE `cad_usuario` (
  `idt_usuario` int(8) NOT NULL AUTO_INCREMENT,
  `nom_usuario` varchar(50) NOT NULL,
  `dsc_email` varchar(30) NOT NULL,
  `flg_ativo` tinyint(1) DEFAULT NULL,
  `dsc_cas_login` varchar(30) DEFAULT NULL,
  `dta_ultimo_login` datetime DEFAULT NULL,
  `flg_online` tinyint(1) DEFAULT NULL,
  PRIMARY KEY (`idt_usuario`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

/*Data for the table `cad_usuario` */

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
