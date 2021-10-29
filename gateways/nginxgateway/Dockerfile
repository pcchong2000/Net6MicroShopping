# ÉèÖÃ»ù´¡¾µÏñ
FROM nginx:alpine

#ÖØÐ´nginx ÅäÖÃ£¬\n\ »»ÐÐ
RUN echo $'server {                                 \n\
    listen       80;                                \n\
    listen  [::]:80;                                \n\
    server_name  localhost;                         \n\
    location /api/order/ {                          \n\
        proxy_pass   http://order.api/;             \n\
    }                                               \n\
    location /api/product/ {                        \n\
        proxy_pass   http://product.api/;           \n\
    }                                               \n\
    location /api/member/ {                         \n\
        proxy_pass   http://member.api/;            \n\
    }                                               \n\
    location /api/pay/ {                            \n\
        proxy_pass   http://pay.api/;               \n\
    }                                               \n\
    location /api/tenant/ {                         \n\
        proxy_pass   http://tenant.api/;            \n\
    }                                               \n\
}                                                   \n\
' >/etc/nginx/conf.d/default.conf

EXPOSE 80
CMD ["nginx", "-g", "daemon off;"]