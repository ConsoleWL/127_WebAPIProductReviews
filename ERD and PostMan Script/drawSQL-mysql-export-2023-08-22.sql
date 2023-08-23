CREATE TABLE `Review`(
    `Id` INT UNSIGNED NOT NULL AUTO_INCREMENT PRIMARY KEY,
    `Text` VARCHAR(255) NOT NULL,
    `Rating` INT NOT NULL,
    `ProductId` INT NOT NULL
);
ALTER TABLE
    `Review` ADD INDEX `review_productid_index`(`ProductId`);
CREATE TABLE `Product`(
    `Id` INT UNSIGNED NOT NULL AUTO_INCREMENT PRIMARY KEY,
    `Name` VARCHAR(255) NOT NULL,
    `Price` DOUBLE NOT NULL
);
ALTER TABLE
    `Review` ADD CONSTRAINT `review_productid_foreign` FOREIGN KEY(`ProductId`) REFERENCES `Product`(`Id`);