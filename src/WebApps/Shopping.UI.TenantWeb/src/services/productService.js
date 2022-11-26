import axios from '../common/request';

const serviceName="/api/product/tenant";
const productService={
    getProductList:(params)    =>axios.get(`${serviceName}/Product`,params),
    getProductDetail:(params)  =>axios.get(`${serviceName}/Product/detail`,params),
    postProduct:(data)        =>axios.post(`${serviceName}/Product`,data),
    putProduct:(data)          =>axios.put(`${serviceName}/Product`,data),
    deleteProduct:(params)  =>axios.delete(`${serviceName}/Product`,params),

    getProductCategoryList:(params)    =>axios.get(`${serviceName}/ProductCategory`,params),
    getProductCategoryDetail:(params)  =>axios.get(`${serviceName}/ProductCategory`,params),
    postProductCategory:(data)        =>axios.post(`${serviceName}/ProductCategory`,data),
    putProductCategory:(data)          =>axios.put(`${serviceName}/ProductCategory`,data),
    deleteProductCategory:(params)  =>axios.delete(`${serviceName}/ProductCategory`,params),

    getStoreProductCategoryList:(params)    =>axios.get(`${serviceName}/StoreProductCategory`,params),
    getStoreProductCategoryDetail:(params)  =>axios.get(`${serviceName}/StoreProductCategory`,params),
    postStoreProductCategory:(data)        =>axios.post(`${serviceName}/StoreProductCategory`,data),
    putStoreProductCategory:(data)          =>axios.put(`${serviceName}/StoreProductCategory`,data),
    deleteStoreProductCategory:(params)  =>axios.delete(`${serviceName}/StoreProductCategory`,params),
};

export default productService;