package authmanager

import (
  "gorm.io/driver/mysql"
  "gorm.io/gorm"
  "math/rand"
  "time"
  "example/hello/logmanager" 
)

// authentication works only with MySQL
// here you can change mysql connection string
const MySQLdns = "root:password@tcp(localhost:3306)/testgo?charset=utf8mb4&parseTime=True&loc=Local"
const TokenSize = 8 //size in characters of the token

// structure of the user table
type User struct {
  WalletId     string
  Password     string
  Token        string 
}

// generate random token, the first time the user try to authenticate
func getRandomToken(n int) (string){
	var charset = []rune("abcdefghijklmnopqrstuvwxyz0123456789")
    rand.Seed(time.Now().UnixNano())
	token := ""
	for i:=0;i<n;i++ {
		token += string(charset[rand.Intn(len(charset))])
	}
	return token;
}

//
// Authorize
// 
// search the user in the user table and check the password parameter
// if the password is ok, and the user has a token, this function returns the token
// if the password is wrong, the function return password wrong error
// if password is ok and token is empty, this function generate a token and put it in the table, and return it
//
// return: token,error
//
func Authorize(ilog logmanager.ILog,walletid string,password string)(token string,err string) {
	ilog.WriteLog("Authorize",0,"params: "+walletid+" "+password)
    db, errOpen := gorm.Open(mysql.Open(MySQLdns), &gorm.Config{})
	if (errOpen == nil){
		user := User{WalletId: walletid, Password: password, Token: ""}
		dbFirst := db.FirstOrCreate(&user, walletid)
		if (dbFirst.Error == nil){
			if (user.Password == password){
				if (user.Token==""){
					user.Token = getRandomToken(TokenSize)
					dbSave := db.Model(&User{}).Where("wallet_id = ?", walletid).Update("Token",user.Token)
					if (dbSave.Error!=nil){
						return "",dbSave.Error.Error()
					} 
				}
				ilog.WriteLogReturn("Authorize",-1,"return: "+user.Token,"OK")
				return user.Token,"OK"
			} else {
				ilog.WriteLogReturn("Authorize",-1,"return: Wrong password","OK")
				return "","Wrong password"
			}
		}  else {
			ilog.WriteLogReturn("Authorize",-1,"return: "+dbFirst.Error.Error(),dbFirst.Error.Error())
		    return "",dbFirst.Error.Error()
		}
	} else {
		return "",errOpen.Error()
	}
}

//
//	CheckAuthorization
//
//  check if the token parameter is the token for that walletid
//  this verify the user
//
//  return true/false
//
func CheckAuthorization(ilog logmanager.ILog,walletid string,token string) (err bool){
	ilog.WriteLog("CheckAuthorization",0,"params: "+walletid+" "+token)
    db, errOpen := gorm.Open(mysql.Open(MySQLdns), &gorm.Config{})
	if (errOpen == nil){
		user := User{WalletId: walletid, Password: "", Token: ""}
		dbFirst := db.First(&user, walletid)
		if (dbFirst.Error == nil){
			if (user.Token == token){
				ilog.WriteLogReturn("CheckAuthorization",-1,"return: true","OK")
				return true
			} 
		}   
	} else {
		ilog.WriteLogError("CheckAuthorization",1,"error: "+errOpen.Error())
	}
	ilog.WriteLogReturn("CheckAuthorization",-1,"return: false","false")
	return false
}