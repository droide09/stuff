package walletmanager

import (
  "github.com/go-redis/redis"
)

// Redis connection parameters
const RedisAddr = "localhost:6379"
const RedisPassword = ""
const RedisDB = 0

type RedisStore struct {
}

func (s RedisStore) Get(walletid string)(balance string,err string) {
	client := redis.NewClient(&redis.Options{
		Addr: RedisAddr,
		Password: RedisPassword,
		DB: RedisDB,
	})
	balance = "0"
	val, intErr := client.Get(walletid).Result()
	if intErr == nil {
		balance = val
		err = "OK"
	} else {
		if (intErr.Error() == "redis: nil"){
		    err = "OK" // wallet is empty
			balance = "0"
		}else{
			err = intErr.Error()
		}
	}
	return balance,err
}

func (s RedisStore) Set(walletid string,amount string) (err string) {
	client := redis.NewClient(&redis.Options{
		Addr: RedisAddr,
		Password: RedisPassword,
		DB: RedisDB,
	})	
	intErr := client.Set(walletid,amount,0).Err()
	if intErr == nil {
		err = "OK"
	} else {
	    err = intErr.Error()
	}
	
	return err
}

