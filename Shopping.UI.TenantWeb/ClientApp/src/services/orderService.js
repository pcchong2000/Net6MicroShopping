import axios from '../common/request';

const serviceName="order";
const orderService={
    getList:(params)    =>axios.get(`/api/${serviceName}/order/tenant`,params),
    getDetail:(params)  =>axios.get(`/api/${serviceName}/order/tenant`,params),
    post:(data)        =>axios.post(`/api/${serviceName}/order/tenant`,data),
    put:(data)          =>axios.put(`/api/${serviceName}/order/tenant`,data),
    delete:(params)  =>axios.delete(`/api/${serviceName}/order/tenant`,params),
};

export default orderService;