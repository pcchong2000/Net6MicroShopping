import axios from '../common/request';

const serviceName="/api/order/tenant";
const orderService={
    getList:(params)    =>axios.get(`${serviceName}/order`,params),
    getDetail:(params)  =>axios.get(`${serviceName}/order`,params),
    post:(data)        =>axios.post(`${serviceName}/order`,data),
    put:(data)          =>axios.put(`${serviceName}/order`,data),
    delete:(params)  =>axios.delete(`${serviceName}/order`,params),
};

export default orderService;