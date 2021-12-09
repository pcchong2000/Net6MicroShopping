import axios from '../common/request';

const serviceName="product";
const productService={
    getList:(params)    =>axios.get(`/api/${serviceName}/product/tenant`,params),
    getDetail:(params)  =>axios.get(`/api/${serviceName}/product/tenant`,params),
    post:(data)        =>axios.post(`/api/${serviceName}/product/tenant`,data),
    put:(data)          =>axios.put(`/api/${serviceName}/product/tenant`,data),
    delete:(params)  =>axios.delete(`/api/${serviceName}/product/tenant`,params),
};

export default productService;