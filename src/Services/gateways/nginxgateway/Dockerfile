# 设置基础镜像
FROM nginx:alpine

#重写nginx 配置，\n\ 换行
RUN echo $'server {                                 \n\
    listen       80;                                \n\
    listen  [::]:80;                                \n\
    server_name  localhost;                         \n\
    location ~* \-in$ {                             \n\
      #内部接口不匹配反向代理                       \n\
    }                                               \n\
    location /file {                                \n\
        proxy_pass   http://shopping.api.oss/api/oss;       \n\
    }                                               \n\
    location /api/oss {                            \n\
        proxy_pass   http://shopping.api.oss;       \n\
    }                                               \n\
    location /api/order {                           \n\
        proxy_pass   http://shopping.api.order;     \n\
    }                                               \n\
    location /api/product {                         \n\
        proxy_pass   http://shopping.api.product;   \n\
    }                                               \n\
    location /api/member {                          \n\
        proxy_pass   http://shopping.api.member;    \n\
    }                                               \n\
    location /api/pay {                             \n\
        proxy_pass   http://shopping.api.pay;       \n\
    }                                               \n\
    location /api/tenant {                          \n\
        proxy_pass   http://shopping.api.tenant;    \n\
    }                                               \n\
}                                                   \n\
' >/etc/nginx/conf.d/default.conf

EXPOSE 80
CMD ["nginx", "-g", "daemon off;"]