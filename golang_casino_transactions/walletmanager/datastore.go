package walletmanager

//
//  IDatastore
//
//  interface for database (Redis, MySQL,etc)
//
type IDatastore interface {
    Get(walletid string)(balance string,err string)  //read balance
    Set(walletid string,amount string) (err string)  //write balance
}