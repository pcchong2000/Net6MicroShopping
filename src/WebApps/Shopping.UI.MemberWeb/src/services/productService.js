import axios from '../common/request';

const serviceName="product";
const productService={
    getProductList:(params)    =>axios.get(`/api/${serviceName}/product`,params),
    getProductDetail:(params)  =>axios.get(`/api/${serviceName}/product/detail`,params),
    postProduct:(data)        =>axios.post(`/api/${serviceName}/product`,data),
    putProduct:(data)          =>axios.put(`/api/${serviceName}/product`,data),
    deleteProduct:(params)  =>axios.delete(`/api/${serviceName}/product`,params),

    getProductCategoryList:(params)    =>axios.get(`/api/${serviceName}/ProductCategory`,params),
    getProductCategoryDetail:(params)  =>axios.get(`/api/${serviceName}/ProductCategory`,params),
    postProductCategory:(data)        =>axios.post(`/api/${serviceName}/ProductCategory`,data),
    putProductCategory:(data)          =>axios.put(`/api/${serviceName}/ProductCategory`,data),
    deleteProductCategory:(params)  =>axios.delete(`/api/${serviceName}/ProductCategory`,params),

};


export default productService;