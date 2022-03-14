package walletmanager

import (
  "gorm.io/driver/mysql"
  "gorm.io/gorm"
)

const MySQLdns = "root:password@tcp(localhost:3306)/testgo?charset=utf8mb4&parseTime=True&loc=Local"

type MysqlStore struct {
}

type Wallet struct {
  WalletId     string
  Amount       string
}

func (s MysqlStore) Get(walletid string)(balance string,err string) {
    db, errOpen := gorm.Open(mysql.Open(MySQLdns), &gorm.Config{})
	if (errOpen == nil){
		wallet := Wallet{WalletId: walletid, Amount: "0"}
		dbFirst := db.FirstOrCreate(&wallet, walletid)
		if (dbFirst.Error==nil){
			balance = wallet.Amount
			return balance,"OK"
		}  else {
		    return "0",dbFirst.Error.Error()
		}
	} else {
		return "0",errOpen.Error()
	}
}

func (s MysqlStore) Set(walletid string,amount string) (err string) {
    db, errOpen := gorm.Open(mysql.Open(MySQLdns), &gorm.Config{})
	if (errOpen == nil){
		dbSave := db.Model(&Wallet{}).Where("wallet_id = ?", walletid).Update("Amount", amount)
		if (dbSave.Error!=nil){
			return dbSave.Error.Error()
		} else {
			return "OK"
		}
	} else {
		return errOpen.Error()
	}
}

