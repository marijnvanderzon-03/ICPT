package main

import (
	"encoding/json"
	"fmt"
	"math/rand"
	"net/http"
	"time"
)

func main() {
	products := []string{"Product1", "Product2", "Product3"}

	for {
		productIndex := rand.Intn(len(products))
		selectedProduct := products[productIndex]
		stock, err := getStock(selectedProduct)
		if err != nil {
			fmt.Printf("Error getting stock for %s: %v\n", selectedProduct, err)
			continue
		}
		fmt.Printf("stock for product %s: %v\n", selectedProduct, stock)
		if stock > 0 {
			err := placeOrder(selectedProduct)
			if err != nil {
				fmt.Printf("Error placing order for %s: %v\n", selectedProduct, err)
			} else {
				fmt.Printf("placed order for %s\n", selectedProduct)
			}
		} else {
			fmt.Printf("%s is out of stock\n", selectedProduct)
		}
		fmt.Printf("\n")
		waitTime := time.Duration(800+rand.Intn(400)) * time.Millisecond
		time.Sleep(waitTime)
	}
}

func getStock(product string) (int, error) {
	resp, err := http.Get(fmt.Sprintf("http://localhost:8080/getStock?product=%s", product))
	if err != nil {
		return 0, err
	}
	defer resp.Body.Close()

	if resp.StatusCode == http.StatusOK {
		var stockData map[string]int
		err := json.NewDecoder(resp.Body).Decode(&stockData)
		if err != nil {
			return 0, err
		}
		return stockData["stock"], nil
	}
	return 0, fmt.Errorf("Failed to get stock for %s. Status code :%d", product, resp.StatusCode)
}

func placeOrder(product string) error {
	resp, err := http.Get(fmt.Sprintf("http://localhost:8080/order?product=%s", product))
	if err != nil {
		return err
	}
	defer resp.Body.Close()

	if resp.StatusCode == http.StatusOK {
		return nil
	}

	return fmt.Errorf("Failed to place order for %s. Status code: %d", product, resp.StatusCode)
}
