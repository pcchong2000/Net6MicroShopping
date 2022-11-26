import axios from '../common/request';

const serviceName="/api/tenant";
const storeService={
    getList:(params)    =>axios.get(`${serviceName}/store`,params),
    getDetail:(params)  =>axios.get(`${serviceName}/store`,params),
    post:(data)        =>axios.post(`${serviceName}/store`,data),
    put:(data)          =>axios.put(`${serviceName}/store`,data),
    delete:(params)  =>axios.delete(`${serviceName}/store`,params),
};

export default storeService;