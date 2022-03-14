package main

import "fmt"
import "github.com/gin-gonic/gin"
import "net/http"
import "example/hello/walletmanager"
import "example/hello/authmanager"
import "example/hello/logmanager"


//
// examples of usage:
//
// curl -X POST http://localhost:8080/api/v1/wallets/333/auth?password=quick
// curl -X POST http://localhost:8080/api/v1/wallets/333/balance?token=fpllngzi
// curl -X POST "http://localhost:8080/api/v1/wallets/333/credit?amount=10.99&token=fpllngzi"
// curl -X POST "http://localhost:8080/api/v1/wallets/333/debit?amount=10.99&token=fpllngzi"
//
// auth endpoint receive a password and the userid is the walletId (333).
// if 333 never authenticated, auth will insert it in the user table and generate a token
// if 333 has been authenticated before, auth will return the token previously generated
// you have to pass the token to the other endpoint
// I know authentication should expire, but for semplicity it never expire, you have to delete the token from the table and auth again
// 
//
func main() {
	router := gin.Default()

	// setup log
    var log logmanager.DefaultLog // write on stdout
	var ilog logmanager.ILog //this is the interface for log
	ilog = log
	
	// setup storage
	// walletmanager.RedisStore store on Redis, walletmanager.MysqlStore store in mysql
	var store walletmanager.MysqlStore
	var istore walletmanager.IDatastore  //this is the interface for storage
    istore = store

	v1 := router.Group("/api/v1/wallets/:walletid")
	{		
		
		v1.POST("/balance", func(c *gin.Context) {	
			ilog.WriteLog("main",0,"called url:"+c.Request.Host+c.Request.URL.Path);	
			walletid := c.Param("walletid")
			if (authmanager.CheckAuthorization(ilog,walletid,c.DefaultQuery("token", ""))){
				balance,err := walletmanager.WmGetBalance(istore,ilog,walletid)			
				c.JSON(http.StatusOK, gin.H{
					"status": err,
					"balance": balance})
			} else {
				c.JSON(http.StatusOK, gin.H{
					"status": "Authorization failed"})
			}
		})
		v1.POST("/credit", func(c *gin.Context) {	
			ilog.WriteLog("main",0,"called url:"+c.Request.Host+c.Request.URL.Path);	
			walletid := c.Param("walletid")
			amount := c.DefaultQuery("amount", "0")
			fmt.Println("amount:"+amount)
			if (authmanager.CheckAuthorization(ilog,walletid,c.DefaultQuery("token", ""))){
				err := walletmanager.WmCredit(istore,ilog,walletid,amount)
				c.JSON(http.StatusOK, gin.H{
					"status": err})
			} else {
				c.JSON(http.StatusOK, gin.H{
					"status": "Authorization failed"})
			}
		})
		v1.POST("/debit",  func(c *gin.Context) {
			ilog.WriteLog("main",0,"called url:"+c.Request.Host+c.Request.URL.Path);	
			walletid := c.Param("walletid")
			if (authmanager.CheckAuthorization(ilog,walletid,c.DefaultQuery("token", ""))){
				err := walletmanager.WmDebit(istore,ilog,walletid,c.DefaultQuery("amount", "0"))			
				c.JSON(http.StatusOK, gin.H{
					"status": err})
			} else {
				c.JSON(http.StatusOK, gin.H{
					"status": "Authorization failed"})
			}
		})
		
		v1.POST("/auth",  func(c *gin.Context) {
		    ilog.WriteLog("main",0,"called url:"+c.Request.Host+c.Request.URL.Path);	
			walletid := c.Param("walletid")
            token, err := authmanager.Authorize(ilog,walletid,c.DefaultQuery("password", ""))			
			c.JSON(http.StatusOK, gin.H{
				"status": err,
				"token": token})
		})
	}

	router.Run(":8080")
}