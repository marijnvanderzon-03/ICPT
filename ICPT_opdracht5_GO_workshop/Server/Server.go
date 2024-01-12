package main

import (
	"encoding/json"
	"fmt"
	"math/rand"
	"net/http"
	"regexp"
	"sync"
	"time"
)

func main() {
	server := NewServer()
	httpHanlder := newHttpHanlder(*server)

	go server.IncreaseStock()

	mux := http.NewServeMux()
	mux.Handle("/getStock", httpHanlder)
	mux.Handle("/order", httpHanlder)

	port := 8080
	fmt.Printf("server started on port %d\n", port)
	http.ListenAndServe(fmt.Sprintf(":%d", port), mux)
}

type Product struct {
	Name       string
	Stock      int
	StockMutex sync.Mutex
}

type Server struct {
	Products     []*Product
	ProductMutex sync.Mutex
}

type httpHanlder struct {
	server Server
}

func NewServer() *Server {
	products := []*Product{
		{Name: "Product1", Stock: 10},
		{Name: "Product2", Stock: 10},
		{Name: "Product3", Stock: 10},
	}

	return &Server{
		Products: products,
	}
}

func newHttpHanlder(s Server) *httpHanlder {
	return &httpHanlder{
		server: s,
	}
}

func (server *Server) IncreaseStock() {
	for {
		time.Sleep(time.Second)
		randomIndex := rand.Intn(len(server.Products))
		product := server.Products[randomIndex]

		product.StockMutex.Lock()
		product.Stock++
		product.StockMutex.Unlock()
	}
}

func (server *Server) GetStockHandler(w http.ResponseWriter, r *http.Request) {
	productName := r.URL.Query().Get("product")

	server.ProductMutex.Lock()
	defer server.ProductMutex.Unlock()

	for _, product := range server.Products {
		if product.Name == productName {
			product.StockMutex.Lock()
			defer product.StockMutex.Unlock()

			w.Header().Set("Content-Type", "application/json")
			json.NewEncoder(w).Encode(map[string]int{"stock": product.Stock})
			return
		}
	}
	http.NotFound(w, r)
}

func (server *Server) OrderHandler(w http.ResponseWriter, r *http.Request) {
	productName := r.URL.Query().Get("product")

	server.ProductMutex.Lock()
	defer server.ProductMutex.Unlock()

	for _, product := range server.Products {
		if product.Name == productName {
			product.StockMutex.Lock()
			defer product.StockMutex.Unlock()

			if product.Stock > 0 {
				product.Stock--
				w.WriteHeader(http.StatusOK)
				w.Write([]byte("order succesfull"))
				return
			} else {
				http.Error(w, "Product not in stock", http.StatusBadRequest)
				return
			}
		}
	}
	http.NotFound(w, r)
}

func (h *httpHanlder) ServeHTTP(w http.ResponseWriter, r *http.Request) {
	var (
		stock = regexp.MustCompile(`^/getStock*$`)
		order = regexp.MustCompile(`^/order*$`)
	)

	switch {
	case stock.MatchString(r.URL.Path):
		h.server.GetStockHandler(w, r)
		return
	case order.MatchString(r.URL.Path):
		h.server.OrderHandler(w, r)
		return
	default:
		w.Write([]byte("this request was not valid"))
		return
	}
}
