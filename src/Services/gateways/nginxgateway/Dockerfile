# ���û�������
FROM nginx:alpine

#��дnginx ���ã�\n\ ����
RUN echo $'server {                                 \n\
    listen       80;                                \n\
    listen  [::]:80;                                \n\
    server_name  localhost;                         \n\
    location ~* \-in$ {                             \n\
      #�ڲ��ӿڲ�ƥ�䷴�����                       \n\
    }                                               \n\
    location /file {                                \n\
        proxy_pass   http://shopping.api.oss;       \n\
    }                                               \n\
    location /api/order/ {                          \n\
        proxy_pass   http://shopping.api.order/;    \n\
    }                                               \n\
    location /api/product/ {                        \n\
        proxy_pass   http://shopping.api.product/;  \n\
    }                                               \n\
    location /api/member/ {                         \n\
        proxy_pass   http://shopping.api.member/;   \n\
    }                                               \n\
    location /api/pay/ {                            \n\
        proxy_pass   http://shopping.api.pay/;      \n\
    }                                               \n\
    location /api/tenant/ {                         \n\
        proxy_pass   http://shopping.api.tenant/;   \n\
    }                                               \n\
}                                                   \n\
' >/etc/nginx/conf.d/default.conf

EXPOSE 80
CMD ["nginx", "-g", "daemon off;"]