import { Environment } from '@abp/ng.core';

const baseUrl = 'http://localhost:4200';

const oAuthConfig = {
  issuer: 'https://localhost:44374/',
  redirectUri: baseUrl,
  clientId: 'SlotManagement_App',
  responseType: 'code',
  scope: 'offline_access SlotManagement',
  requireHttps: true,
};

export const environment = {
  production: false,
  application: {
    baseUrl,
    name: 'SlotManagement',
  },
  oAuthConfig,
  apis: {
    default: {
      url: 'https://localhost:44374',
      rootNamespace: 'SlotManagement',
    },
    AbpAccountPublic: {
      url: oAuthConfig.issuer,
      rootNamespace: 'AbpAccountPublic',
    },
  },
} as Environment;
