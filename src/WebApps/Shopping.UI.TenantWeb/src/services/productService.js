import axios from '../common/request';

const serviceName="product";
const productService={
    getProductList:(params)    =>axios.get(`/api/${serviceName}/Product/tenant`,params),
    getProductDetail:(params)  =>axios.get(`/api/${serviceName}/Product/tenant/detail`,params),
    postProduct:(data)        =>axios.post(`/api/${serviceName}/Product/tenant`,data),
    putProduct:(data)          =>axios.put(`/api/${serviceName}/Product/tenant`,data),
    deleteProduct:(params)  =>axios.delete(`/api/${serviceName}/Product/tenant`,params),

    getProductCategoryList:(params)    =>axios.get(`/api/${serviceName}/ProductCategory/tenant`,params),
    getProductCategoryDetail:(params)  =>axios.get(`/api/${serviceName}/ProductCategory/tenant`,params),
    postProductCategory:(data)        =>axios.post(`/api/${serviceName}/ProductCategory/tenant`,data),
    putProductCategory:(data)          =>axios.put(`/api/${serviceName}/ProductCategory/tenant`,data),
    deleteProductCategory:(params)  =>axios.delete(`/api/${serviceName}/ProductCategory/tenant`,params),

    getStoreProductCategoryList:(params)    =>axios.get(`/api/${serviceName}/StoreProductCategory/tenant`,params),
    getStoreProductCategoryDetail:(params)  =>axios.get(`/api/${serviceName}/StoreProductCategory/tenant`,params),
    postStoreProductCategory:(data)        =>axios.post(`/api/${serviceName}/StoreProductCategory/tenant`,data),
    putStoreProductCategory:(data)          =>axios.put(`/api/${serviceName}/StoreProductCategory/tenant`,data),
    deleteStoreProductCategory:(params)  =>axios.delete(`/api/${serviceName}/StoreProductCategory/tenant`,params),
};

export default productService;