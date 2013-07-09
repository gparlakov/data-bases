SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0;
SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0;
SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='TRADITIONAL,ALLOW_INVALID_DATES';

CREATE SCHEMA IF NOT EXISTS `Univerisities` DEFAULT CHARACTER SET utf8 COLLATE utf8_general_ci ;
USE `Univerisities` ;

-- -----------------------------------------------------
-- Table `Univerisities`
-- -----------------------------------------------------
CREATE  TABLE IF NOT EXISTS `Univerisities` (
  `Id` INT NOT NULL ,
  `Name` VARCHAR(80) NULL ,
  PRIMARY KEY (`Id`) )
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `mydb`.`Faculties`
-- -----------------------------------------------------
CREATE  TABLE IF NOT EXISTS `Faculties` (
  `Id` INT NOT NULL ,
  `Name` VARCHAR(50) NULL ,
  `Univerisities_Id` INT NOT NULL ,
  PRIMARY KEY (`Id`) ,
  INDEX `fk_Faculties_Univerisities_idx` (`Univerisities_Id` ASC) ,
  CONSTRAINT `fk_Faculties_Univerisities`
    FOREIGN KEY (`Univerisities_Id` )
    REFERENCES `Univerisities` (`Id` )
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `mydb`.`Departments`
-- -----------------------------------------------------
CREATE  TABLE IF NOT EXISTS `Departments` (
  `Id` INT NOT NULL ,
  `Name` VARCHAR(45) NULL ,
  `Faculties_Id` INT NOT NULL ,
  PRIMARY KEY (`Id`) ,
  INDEX `fk_Departments_Faculties1_idx` (`Faculties_Id` ASC) ,
  CONSTRAINT `fk_Departments_Faculties1`
    FOREIGN KEY (`Faculties_Id` )
    REFERENCES `mydb`.`Faculties` (`Id` )
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `mydb`.`Professors`
-- -----------------------------------------------------
CREATE  TABLE IF NOT EXISTS `Professors` (
  `Id` INT NOT NULL ,
  `Name` VARCHAR(45) NULL ,
  `Departments_Id` INT NOT NULL ,
  PRIMARY KEY (`Id`) ,
  INDEX `fk_Professors_Departments1_idx` (`Departments_Id` ASC) ,
  CONSTRAINT `fk_Professors_Departments1`
    FOREIGN KEY (`Departments_Id` )
    REFERENCES `mydb`.`Departments` (`Id` )
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB
COMMENT = '	';


-- -----------------------------------------------------
-- Table `mydb`.`Courses`
-- -----------------------------------------------------
CREATE  TABLE IF NOT EXISTS `Courses` (
  `Id` INT NOT NULL ,
  `Name` VARCHAR(45) NULL ,
  `Departments_Id` INT NOT NULL ,
  PRIMARY KEY (`Id`) ,
  INDEX `fk_Courses_Departments1_idx` (`Departments_Id` ASC) ,
  CONSTRAINT `fk_Courses_Departments1`
    FOREIGN KEY (`Departments_Id` )
    REFERENCES `mydb`.`Departments` (`Id` )
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `mydb`.`CoursesAndProfessors`
-- -----------------------------------------------------
CREATE  TABLE IF NOT EXISTS `CoursesAndProfessors` (
  `Courses_Id` INT NOT NULL ,
  `Professors_Id` INT NOT NULL ,
  INDEX `fk_CoursesAndProfessors_Courses1_idx` (`Courses_Id` ASC) ,
  INDEX `fk_CoursesAndProfessors_Professors1_idx` (`Professors_Id` ASC) ,
  CONSTRAINT `fk_CoursesAndProfessors_Courses1`
    FOREIGN KEY (`Courses_Id` )
    REFERENCES `mydb`.`Courses` (`Id` )
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT `fk_CoursesAndProfessors_Professors1`
    FOREIGN KEY (`Professors_Id` )
    REFERENCES `mydb`.`Professors` (`Id` )
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `mydb`.`Titles`
-- -----------------------------------------------------
CREATE  TABLE IF NOT EXISTS `Titles` (
  `Id` INT NOT NULL ,
  `Name` VARCHAR(45) NULL ,
  PRIMARY KEY (`Id`) )
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `mydb`.`ProfessorsAndTitles`
-- -----------------------------------------------------
CREATE  TABLE IF NOT EXISTS `ProfessorsAndTitles` (
  `Professors_Id` INT NOT NULL ,
  `Titles_Id` INT NOT NULL ,
  INDEX `fk_ProfessorsAndTitles_Professors1_idx` (`Professors_Id` ASC) ,
  INDEX `fk_ProfessorsAndTitles_Titles1_idx` (`Titles_Id` ASC) ,
  CONSTRAINT `fk_ProfessorsAndTitles_Professors1`
    FOREIGN KEY (`Professors_Id` )
    REFERENCES `mydb`.`Professors` (`Id` )
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT `fk_ProfessorsAndTitles_Titles1`
    FOREIGN KEY (`Titles_Id` )
    REFERENCES `mydb`.`Titles` (`Id` )
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `mydb`.`Students`
-- -----------------------------------------------------
CREATE  TABLE IF NOT EXISTS `Students` (
  `Id` INT NOT NULL ,
  `Name` VARCHAR(45) NULL ,
  `Faculties_Id` INT NOT NULL ,
  PRIMARY KEY (`Id`) ,
  INDEX `fk_Students_Faculties1_idx` (`Faculties_Id` ASC) ,
  CONSTRAINT `fk_Students_Faculties1`
    FOREIGN KEY (`Faculties_Id` )
    REFERENCES `mydb`.`Faculties` (`Id` )
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `mydb`.`StudentsAndCourses`
-- -----------------------------------------------------
CREATE  TABLE IF NOT EXISTS `StudentsAndCourses` (
  `Students_Id` INT NOT NULL ,
  `Courses_Id` INT NOT NULL ,
  INDEX `fk_StudentsAndCourses_Students1_idx` (`Students_Id` ASC) ,
  INDEX `fk_StudentsAndCourses_Courses1_idx` (`Courses_Id` ASC) ,
  CONSTRAINT `fk_StudentsAndCourses_Students1`
    FOREIGN KEY (`Students_Id` )
    REFERENCES `mydb`.`Students` (`Id` )
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT `fk_StudentsAndCourses_Courses1`
    FOREIGN KEY (`Courses_Id` )
    REFERENCES `mydb`.`Courses` (`Id` )
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;

USE `mydb` ;


SET SQL_MODE=@OLD_SQL_MODE;
SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS;
SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS;
