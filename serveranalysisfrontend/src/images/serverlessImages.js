import React from 'react';
import * as serverlessImages from "./serverless";

const ServerlessImages = () => {
  return (
    <div>
      <img src={serverlessImages.serverlessIntroduction} alt="Introduction" />
      <img src={serverlessImages.serverlessAdvantages} alt="Advantages" />
      <img src={serverlessImages.serverlessApproach} alt="Approach" />
      <img src={serverlessImages.serverlessUseCases} alt="Use Cases" />
      <img src={serverlessImages.serverlessLimitations} alt="Limitations" />
      <img src={serverlessImages.serverlessComparison} alt="Comparison" />
      <img src={serverlessImages.serverlessIndustryInsights} alt="Industry Insights" />
      <img src={serverlessImages.serverlessBeneficiaries} alt="Beneficiaries" />
    </div>
  );
};

export default ServerlessImages;