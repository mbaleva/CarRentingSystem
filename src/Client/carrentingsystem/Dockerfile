FROM node:12.20 as build
WORKDIR /app
ENV PATH /app/node_modules/.bin:$PATH
COPY ./src/Client/carrentingsystem/package.json /app/package.json
RUN npm install
RUN npm install -g @angular/cli@10.1.2
COPY /src/Client/carrentingsystem /app
ARG configuration=production
RUN ng build --output-path=dist --configuration=$configuration
FROM nginx:1.16.0-alpine
RUN rm -rf /usr/share/nginx/html/*
COPY src/Client/carrentingsystem/nginx.conf /etc/nginx/nginx.conf
COPY --from=build /app/dist /usr/share/nginx/html
EXPOSE 80
CMD ["nginx", "-g", "daemon off;"]